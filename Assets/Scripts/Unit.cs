using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    [SerializeField] private int actionPointsMax;
    [SerializeField] private bool isEnemy;

    private GridPosition gridPosition;
    private MoveAction moveAction;
    private SpinAction spinAction;
    private int actionPoints;

    private BaseAction[] baseActionArray;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>(); 
        baseActionArray = GetComponents<BaseAction>();
    }
    private void Start()
    {
        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
        
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        actionPoints = actionPointsMax;
    }

    private void Update()
    {
        


        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }

    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public SpinAction GetSpinAction()
    {
        return spinAction;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }

    public bool TryTakeActionPointFofAction(BaseAction baseAction)
    {
        if (CanTakeActionPointForAction(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointCost());
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanTakeActionPointForAction(BaseAction baseAction)
    {
        if (actionPoints >= baseAction.GetActionPointCost())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;
    }

    public int GetActionPoints()
    {
        return actionPoints;
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        if ((IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) ||
        (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn()))
        {
            actionPoints = actionPointsMax;
        }
        
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public void Damage()
    {
        Debug.Log("hit");
    }
}
