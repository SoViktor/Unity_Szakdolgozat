using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    [SerializeField] private int actionPointsMax;
    [SerializeField] private bool isEnemy;

    private StatSystem statSystem;
    private GridPosition gridPosition;
    private MoveAction moveAction;
    private int actionPoints;

    private BaseAction[] baseActionArray;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        baseActionArray = GetComponents<BaseAction>();
        statSystem = GetComponent<StatSystem>();

    }
    private void Start()
    {
        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        statSystem.OnDeath += StatSystem_OnDeath;
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

    public void Damage(bool isMagical, DamageTypes damageType, float damageValue, float attackStat)
    {
        statSystem.Damage(isMagical, damageType, damageValue,attackStat );
    }

    public float GetAttackStat()
    {
        return statSystem.GetAttack();
    }

    public float GetMagicAttackStat()
    {
        return statSystem.GetMagicAttack();
    }

    public int GetMoveDistanceStat()
    {
        return statSystem.GetMoveDistance();
    }

    private void StatSystem_OnDeath(object sender, EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);


        Destroy(gameObject);
    }

}
