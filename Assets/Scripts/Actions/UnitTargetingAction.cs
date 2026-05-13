using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitTargetingAction : BaseAction
{
    protected enum State
    {
        Ready,
        Action,
        Finished,
    }
    protected State state;
    protected float stateTimer;
    protected bool canDoAction;

    protected Unit targetUnit;

    protected abstract int Range {get;}


    protected void Update()
    {
        if (!isActive)
        {
            return;
        }
        stateTimer -= Time.deltaTime;
        switch (state)
        {
            case State.Ready:

                DoReady();

                break;
            case State.Action:
            if (canDoAction)
            {
                canDoAction = false;
                DoAction();
            }

                break;
            case State.Finished:

                DoFinished();

                break;
        }

        if (stateTimer <= 0f)
        {
            NextState();
        }
    }


    protected virtual void NextState()
    {
        switch (state)
        {
            case State.Ready:

                state = State.Action;
                float attackStateTimer = 0.2f;
                stateTimer = attackStateTimer;

                break;
            case State.Action:

                state = State.Finished;
                float finishedStateTimer = 0.3f;
                stateTimer = finishedStateTimer;

                break;
            case State.Finished:

                ActionComplete();

                break;
        }
    }

    protected abstract void DoAction();

    protected virtual void DoFinished()
    {
        return;
    }

    protected virtual void SetUpReady()
    {
        state = State.Ready;
        float readystateTimer = 1f;
        stateTimer = readystateTimer;

        canDoAction = true;
    }

    protected virtual void DoReady()
    {
        Vector3 targetUnitDirection = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;
        float rotationSpeed = 10f; 
        transform.forward = Vector3.Lerp(transform.forward, targetUnitDirection, Time.deltaTime * rotationSpeed);

    }

    public int GetRange()
    {
        return Range;
    }



}
