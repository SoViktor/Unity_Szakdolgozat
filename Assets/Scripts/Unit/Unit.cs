using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    public static event EventHandler OnAnyUnitDied;
    [SerializeField] private int actionPointsMax;
    [SerializeField] private bool isEnemy;

    private StatSystem statSystem;
    private GridPosition gridPosition;
    private MoveAction moveAction;
    private int actionPoints;

    private BaseAction[] baseActionArray;

    private void Awake()
    {
        SetUpUnit();
    }
    private void Start()
    {
        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        
        statSystem.OnDeath += StatSystem_OnDeath;
        actionPoints = actionPointsMax;
    }

    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            GridPosition currentGridPosition = gridPosition;
            gridPosition = newGridPosition;
 
            LevelGrid.Instance.UnitMovedGridPosition(this, currentGridPosition, newGridPosition);
        }

    }

    public void SetUpUnit()
    {
        moveAction = GetComponent<MoveAction>();
        baseActionArray = GetComponents<BaseAction>();
        statSystem = GetComponent<StatSystem>();
    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
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

    public StatSystem GetStatSystem()
    {
        return statSystem;
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

    public void ResetActionPoints()
    {
        actionPoints = actionPointsMax;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public void Damage(bool isMagical, DamageTypes damageType, float damageValue, float attackStat)
    {
        statSystem.Damage(isMagical, damageType, damageValue,attackStat );
    }
 

    private void StatSystem_OnDeath(object sender, EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);
        OnAnyUnitDied?.Invoke(this, EventArgs.Empty);


        Destroy(gameObject);
    }


}
