using System;
using System.Collections.Generic;
using UnityEngine;

public class SummonAction : UnitTargetingAction
{
    [SerializeField] Unit summonedUnit;

    public event EventHandler<ActionArgsWithTwoUnits> OnNewUnitSummoned;
    public static event EventHandler OnSummonFinished;
 
    private Vector3 summonWorldPosition;

    protected override bool IsCirculiar => false;

    protected override int Range => 3;

    protected override ActionTypes ActionType => ActionTypes.SummonAction;

    public override string GetActionName()
    {
        return "Summon" + summonedUnit;
    }

    public override List<GridPosition> GetValidGridPositionList()
        {
            List<GridPosition> validGridPositionList = new();
            GridPosition unitGridPosition = unit.GetGridPosition();

            for (int x = -Range; x <= Range; x++)
            {
                for (int z = -Range; z <= Range; z++)
                {
                    GridPosition offetGridPosition = new(x,z);
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

        SetUpReady();

        return;
    }

    public override int GetActionPointCost()
    {
        return 2;
    }

    protected override void DoAction()
    {
        Unit newUnit = Instantiate(summonedUnit, summonWorldPosition, Quaternion.identity);
        TurnSystem.Instance.AddUnitToTurnSystem(newUnit); 

        OnNewUnitSummoned?.Invoke(this, new ActionArgsWithTwoUnits{targetUnit = newUnit, activeUnit = unit} );

    }

    protected override void DoReady()
    {
        Vector3 summonedUnitDirection = (summonWorldPosition - unit.GetWorldPosition()).normalized;
        float rotationSpeed = 10f; 
        transform.forward = Vector3.Lerp(transform.forward, summonedUnitDirection, Time.deltaTime * rotationSpeed);

    }

    protected override void DoFinished()
    {
        OnSummonFinished?.Invoke(this, EventArgs.Empty);
    }
}
