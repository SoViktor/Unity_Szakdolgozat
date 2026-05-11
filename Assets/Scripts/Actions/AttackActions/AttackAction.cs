using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackAction : UnitTargetingAction
{
    protected abstract DamageTypes damageType {get;}
    protected abstract bool isMagical{get;}
    protected abstract float attackValue{get;}
    protected float attackStat;


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
                if (unitGridPosition == testGridPosition)
                {
                    continue;
                }
                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }
                if (isCirculiar)
                {
                    int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                    if (testDistance > range)
                    {
                        continue;
                    }
                }

                /*Unit testTargetUnit = LevelGrid.Instance.GetUnitOnGridPosition(testGridPosition);
                
                if (testTargetUnit.IsEnemy() == unit.IsEnemy())
                {
                    continue;
                }*/

                validGridPositionList.Add(testGridPosition);
            }

        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);
        targetUnit = LevelGrid.Instance.GetUnitOnGridPosition(gridPosition);

        SetUpReady();
        
    }

    protected void SetAttackStat()
    {
        StatSystem statSystem = unit.GetStatSystem();
        if (isMagical)
        {
            attackStat = statSystem.GetMagicAttack();
        }
        else
        {
            attackStat = statSystem.GetAttack();
        }
    }

    protected void Attack()
    {
        SetAttackStat();
        StatSystem targetUnitStateSystem = targetUnit.GetStatSystem();
        targetUnitStateSystem.Damage(isMagical, damageType, attackValue, attackStat );
    }

}
