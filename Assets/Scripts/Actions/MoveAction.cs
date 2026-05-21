using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private readonly float stoppingDistance = 0.1f;

    public event EventHandler OnStartMoveAction;
    public event EventHandler OnStopMoveAction;


    private Vector3 targetPosition;

    protected override bool IsCirculiar => false;

    protected override ActionTypes ActionType => ActionTypes.MoveAction;

    protected override void Awake()
    {
        base.Awake();
        targetPosition = transform.position;
    }


    private void Update()
    {
        if(!isActive)
        {
            return;
        }
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
        transform.position += moveSpeed * Time.deltaTime * moveDirection;    
            
        } else
        {
            OnStopMoveAction?.Invoke(this, EventArgs.Empty);
            ActionComplete();
        }
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
        
    }

    public override void TakeAction (GridPosition gridPosition, Action onMoveComplete)
    {
        ActionStart(onMoveComplete);
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        OnStartMoveAction?.Invoke(this, EventArgs.Empty);
    }

    public override List<GridPosition> GetValidGridPositionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();
            StatSystem statSystem = unit.GetStatSystem();

            int range = statSystem.GetMoveRange();

            for (int x = -range; x <= range; x++)
            {
                for (int z = -range; z <= range; z++)
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
                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                    {
                        continue;
                    }

                    validGridPositionList.Add(testGridPosition);
                }

            }

            return validGridPositionList;
        }

    public override string GetActionName()
    {
        return "Move";
    }
 }
