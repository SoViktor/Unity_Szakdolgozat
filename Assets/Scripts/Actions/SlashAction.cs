using System;
using System.Collections.Generic;
using UnityEngine;

public class SlashAction : BaseAction
{
<<<<<<< Updated upstream
=======
    private enum State
    {
        Ready,
        Attack,
        Finished,
    }

    public event EventHandler OnStartSlashAction;
>>>>>>> Stashed changes
    private int maxSlashingDistance = 1;

    private float totalFloatAmount;
    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0,spinAddAmount,0);
        totalFloatAmount += spinAddAmount;
        if (totalFloatAmount >= 360)
        {
            isActive = false;
            onActionComplete();
        }
<<<<<<< Updated upstream
=======

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
        OnStartSlashAction?.Invoke(this, EventArgs.Empty);
        targetUnit.Damage();
>>>>>>> Stashed changes
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

                    validGridPositionList.Add(testGridPosition);
                }

            }

            return validGridPositionList;
            }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        isActive = true;
        totalFloatAmount = 0;
    }

}
