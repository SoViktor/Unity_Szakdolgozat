using System;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
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
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        isActive = true;
        totalFloatAmount = 0;
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition>
        {
            unitGridPosition
        };
    }

    public override int GetActionPointCost()
    {
        return 2;
    }

}
