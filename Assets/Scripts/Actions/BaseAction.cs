using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected abstract bool IsCirculiar {get;}
    protected Unit unit;
    protected bool isActive;

    protected Action onActionComplete;

    protected abstract ActionTypes ActionType{get;}


    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public abstract string GetActionName();

    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPosition = GetValidGridPositionList();
        return validGridPosition.Contains(gridPosition);
 
    }

    public abstract List<GridPosition> GetValidGridPositionList();

    public virtual int GetActionPointCost()
    {
        return 1;
    }

    protected void ActionStart(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
    }

    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();
    }

    public bool GetIsCirculiar()
    {
        return IsCirculiar;
    }

    public ActionTypes GetActionTypes()
    {
        return ActionType;
    }

}
