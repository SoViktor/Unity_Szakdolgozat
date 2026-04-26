using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackAction : BaseAction
{
    protected enum State
    {
        Ready,
        Attack,
        Finished,
    }
    protected abstract int maxAttackDistance {get;} 
    protected abstract DamageTypes damageType {get;}
    protected abstract bool isMagical{get;}
    protected abstract float attackValue{get;}
    protected abstract bool isAttackCirculiar {get;}
    protected float attackStat;
    protected State state;

    protected float stateTimer;
    protected bool canAttack;
    protected Unit targetUnit;


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

                Vector3 enemyDirection = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;
                float rotationSpeed = 10f; 
                transform.forward = Vector3.Lerp(transform.forward, enemyDirection, Time.deltaTime * rotationSpeed);

                break;
            case State.Attack:
            if (canAttack)
            {
                canAttack = false;
                SetAttackStat();
                Attack();
            }

                break;
            case State.Finished:

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

                state = State.Attack;
                float attackStateTimer = 0.2f;
                stateTimer = attackStateTimer;

                break;
            case State.Attack:

                state = State.Finished;
                float finishedStateTimer = 0.3f;
                stateTimer = finishedStateTimer;

                break;
            case State.Finished:

                ActionComplete();

                break;
        }
    }

    protected abstract void Attack();

    public override List<GridPosition> GetValidGridPositionList()
    {

        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxAttackDistance; x <= maxAttackDistance; x++)
        {
            for (int z = -maxAttackDistance; z <= maxAttackDistance; z++)
            {
                GridPosition offetGridPosition = new GridPosition (x,z);
                GridPosition testGridPosition = unitGridPosition + offetGridPosition;

                if(!LevelGrid.Instance.IsValidPosition(testGridPosition))
                {
                    continue;
                }
                if (unitGridPosition == testGridPosition)
                {
                    continue;
                }
                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }
                if (isAttackCirculiar)
                {
                    int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                    if (testDistance > maxAttackDistance)
                    {
                        continue;
                    }
                }

                Unit testTargetUnit = LevelGrid.Instance.GetUnitOnGridPosition(testGridPosition);
                
                if (testTargetUnit.IsEnemy() == unit.IsEnemy())
                {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }

        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);
        targetUnit = LevelGrid.Instance.GetUnitOnGridPosition(gridPosition);

        state = State.Ready;
        float readystateTimer = 1f;
        stateTimer = readystateTimer;

        canAttack = true;
        
    }

    protected void SetAttackStat()
    {
        if (isMagical)
        {
            attackStat = unit.GetAttackStat();
        }
        else
        {
            attackStat = unit.GetMagicAttackStat();
        }
    }



}
