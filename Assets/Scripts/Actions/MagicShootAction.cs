using System;
using System.Collections.Generic;
using UnityEngine;

public class MagicShootAction : BaseAction
{    private enum State
    {
        Ready,
        Attack,
        Finished,
    }

    public event EventHandler<OnStartMagicShootActionArgs> OnStartMagicShootAction;

    public class OnStartMagicShootActionArgs : EventArgs
    {
        public Unit targetUnit;
        public Unit attackingUnit;
    }
    
    private int maxMagicShootDistance = 5;
    private State state;
    private float stateTimer;
    private Unit targetUnit;
    private bool canMagicShoot;
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
            if (canMagicShoot)
            {
                canMagicShoot = false;
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

                break;
            case State.Finished:

                ActionComplete();

                break;
        }
        Debug.Log(state);
    }

    private void Attack()
    {
        OnStartMagicShootAction?.Invoke(this, new OnStartMagicShootActionArgs{targetUnit =targetUnit, attackingUnit = unit});
        
        targetUnit.Damage();
    }

    public override string GetActionName()
    {
        return "Magic Shoot";
    }

    public override List<GridPosition> GetValidGridPositionList()
    {

            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();

            for (int x = -maxMagicShootDistance; x <= maxMagicShootDistance; x++)
            {
                for (int z = -maxMagicShootDistance; z <= maxMagicShootDistance; z++)
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

                    int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                    if (testDistance > maxMagicShootDistance)
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

        canMagicShoot = true;
        
        Debug.Log(state);


    }
}
