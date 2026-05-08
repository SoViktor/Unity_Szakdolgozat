using System;
using System.Collections.Generic;
using UnityEngine;

public class SummonAction : BaseAction
{
    [SerializeField] Unit summonedUnit;
    [SerializeField] int summonRange;

    public event EventHandler<ActionArgsWithTwoUnits> OnNewUnitSummoned;


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

        Vector3 summonWorldPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);

        Unit newUnit = Instantiate(summonedUnit, summonWorldPosition, Quaternion.identity);

        TurnSystem.Instance.AddUnitToTurnSystem(newUnit);
        
        OnNewUnitSummoned?.Invoke(this, new ActionArgsWithTwoUnits{activeUnit = unit, targetUnit = newUnit} );

        ActionComplete();

        return;
    }
}
