using System;
using System.Collections.Generic;
using UnityEngine;

public class SummonAction : UnitTargetingAction
{
    [SerializeField] Unit summonedUnit;

    public event EventHandler<ActionArgsWithTwoUnits> OnNewUnitSummoned;
    public static event EventHandler OnSummonFinished;
 
    private Vector3 summonWorldPosition;

    protected override bool isCirculiar => false;

    protected override int range => 3;

    public override string GetActionName()
    {
        return "Summon" + summonedUnit;
    }

    public override List<GridPosition> GetValidGridPositionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();

            for (int x = -range; x <= range; x++)
            {
                for (int z = -range; z <= range; z++)
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
}
