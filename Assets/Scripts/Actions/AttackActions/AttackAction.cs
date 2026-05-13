using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackAction : UnitTargetingAction
{
    protected abstract DamageTypes DamageType {get;}
    protected abstract bool IsMagical{get;}
    protected abstract float AttackValue{get;}
    protected float attackStat;
    protected override ActionTypes ActionType => ActionTypes.AttackAction;



    public override List<GridPosition> GetValidGridPositionList()
    {
        List<GridPosition> validGridPositionList = new();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -Range; x <= Range; x++)
        {
            for (int z = -Range; z <= Range; z++)
            {
                GridPosition offsetGridPosition = new(x,z);
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
                if (IsCirculiar)
                {
                    int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                    if (testDistance > Range)
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
        if (IsMagical)
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
        targetUnitStateSystem.Damage(IsMagical, DamageType, AttackValue, attackStat );
    }

}
