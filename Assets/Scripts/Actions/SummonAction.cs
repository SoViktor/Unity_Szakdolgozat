using System;
using System.Collections.Generic;
using UnityEngine;

public class SummonAction : BaseAction
{
    private enum State
    {
        Ready,
        Summon,
        Finished,
    }
    [SerializeField] Unit summonedUnit;
    [SerializeField] int summonRange;

    public event EventHandler<ActionArgsWithTwoUnits> OnNewUnitSummoned;
    public static event EventHandler OnSummonFinished;
 

    private State state;
    private float stateTimer;
    private bool canSummon;
    private Vector3 summonWorldPosition;

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

                Vector3 summonedUnitDirection = (summonWorldPosition - unit.GetWorldPosition()).normalized;
                float rotationSpeed = 10f; 
                transform.forward = Vector3.Lerp(transform.forward, summonedUnitDirection, Time.deltaTime * rotationSpeed);

                break;
            case State.Summon:
            if (canSummon)
            {
                canSummon = false;

                Unit newUnit = Instantiate(summonedUnit, summonWorldPosition, Quaternion.identity);

                TurnSystem.Instance.AddUnitToTurnSystem(newUnit);
        
                OnNewUnitSummoned?.Invoke(this, new ActionArgsWithTwoUnits{targetUnit = newUnit, activeUnit = unit} );
            }

                break;
            case State.Finished:

                OnSummonFinished?.Invoke(this, EventArgs.Empty);
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

                state = State.Summon;
                float SummonStateTimer = 0.2f;
                stateTimer = SummonStateTimer;

                break;
            case State.Summon:

                state = State.Finished;
                float finishedStateTimer = 0.3f;
                stateTimer = finishedStateTimer;

                break;
            case State.Finished:

                ActionComplete();

                break;
        }
    }


    public override string GetActionName()
    {
        return "Summon" + summonedUnit;
    }

    public override List<GridPosition> GetValidGridPositionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();

            for (int x = -summonRange; x <= summonRange; x++)
            {
                for (int z = -summonRange; z <= summonRange; z++)
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
                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                    {
                        continue;
                    }

                    validGridPositionList.Add(testGridPosition);
                }

            }

            return validGridPositionList;
        }

    public override void TakeAction (GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);

        summonWorldPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);

        state = State.Ready;
        float readystateTimer = 1f;
        stateTimer = readystateTimer;

        canSummon = true;

        return;
    }

    public override int GetActionPointCost()
    {
        return 2;
    }
}
