using System;
using System.Collections.Generic;
using UnityEngine;

public class SlashAction : BaseAction
{
    private enum State
    {
        Ready,
        Attack,
        Finished,
    }

    public event EventHandler OnStartSlashAction;
    public event EventHandler OnStopSlashAction;

    private int maxSlashingDistance = 1;
    private State state;
    private float stateTimer;
    private Unit targetUnit;
    private bool canSlash;
    private void Update()
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
            if (canSlash)
            {
                canSlash = false;
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

    private void NextState()
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
                OnStopSlashAction?.Invoke(this, EventArgs.Empty);


                break;
            case State.Finished:

                ActionComplete();

                break;
        }
        Debug.Log(state);
    }

    private void Attack()
    {
        OnStartSlashAction?.Invoke(this, EventArgs.Empty);
        targetUnit.Damage();
    }

    public override string GetActionName()
    {
        return "slash";
    }

    public override List<GridPosition> GetValidGridPositionList()
    {

            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();

            for (int x = -maxSlashingDistance; x <= maxSlashingDistance; x++)
            {
                for (int z = -maxSlashingDistance; z <= maxSlashingDistance; z++)
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

        canSlash = true;
        
        Debug.Log(state);


    }

}
