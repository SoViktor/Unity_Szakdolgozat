using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SupportAction : UnitTargetingAction
{

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);
        targetUnit = LevelGrid.Instance.GetUnitOnGridPosition(gridPosition);

        SetUpReady();
    }
    public override List<GridPosition> GetValidGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                GridPosition offsetGridPosition = new GridPosition (x,z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if(!LevelGrid.Instance.IsValidPosition(testGridPosition))
                {
                    continue;
                }

                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }


                Unit testTargetUnit = LevelGrid.Instance.GetUnitOnGridPosition(testGridPosition);
                
                if (testTargetUnit.IsEnemy() != unit.IsEnemy())
                {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }

        }

        return validGridPositionList;
    }    


}
