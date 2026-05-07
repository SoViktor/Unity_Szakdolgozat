using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{

    [Serializable]
    public struct GridVisualVariantsMaterial
    {
        public GridVisualVariants gridVisualVariant;
        public Material material;
    }
    public static GridSystemVisual Instance {get; private set;}
    [SerializeField] private Transform gridSystemVisualSingleGrid;
    [SerializeField] private List<GridVisualVariantsMaterial> gridVisualVariantsMaterialList;

    private GridSystemVisualSingel[,] gridSystemVisualSingelArray;
        private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("To Many GridSystemVisual! Only one Allowed" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }
    
    private void Start()
    {
        gridSystemVisualSingelArray = new GridSystemVisualSingel[
            LevelGrid.Instance.GetWidth(),
            LevelGrid.Instance.GetLength()];
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetLength(); z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                Transform gridSystemVisualSingelTransform = 
                    Instantiate(gridSystemVisualSingleGrid, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                gridSystemVisualSingelArray[x,z] = gridSystemVisualSingelTransform.GetComponent<GridSystemVisualSingel>();
            }
        }

        LevelGrid.Instance.OnUnitChangedGridPosition += LevelGrid_OnUnitChangedGridPosition;
        UnitActionSystem.Instance.OnSelectedActionChange += UnitActionSystem_OnSelectedActionChange;
        Unit.OnAnyUnitDied += Unit_OnAnyUnitDied;

    }


    public void HideAllGridPosition()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetLength(); z++)
            {
                gridSystemVisualSingelArray[x,z].Hide();
            }
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList, GridVisualVariants gridVisualVariant)
    {
        foreach (GridPosition gridPosition in gridPositionList)
        {
            gridSystemVisualSingelArray[gridPosition.x, gridPosition.z].Show(GetGridVisualVariantsMaterial(gridVisualVariant));
        }
    }

    private void ShowAttackRange(GridPosition unitGridPosition, int maxAttackDistance, bool isAttackCirculiar, GridVisualVariants gridVisualVariant)
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        for (int x = -maxAttackDistance; x <= maxAttackDistance; x++)
        {
            for (int z = -maxAttackDistance; z <= maxAttackDistance; z++)
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

                if (isAttackCirculiar)
                {
                    int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                    if (testDistance > maxAttackDistance)
                    {
                        continue;
                    }
                }

                validGridPositionList.Add(testGridPosition);
            }

        }


        ShowGridPositionList(validGridPositionList, gridVisualVariant);
    }

    private void UpdateGridVisual()
    {
        HideAllGridPosition();


       BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();
       Unit activeUnit = TurnSystem.Instance.GetActiveUnit();

       GridVisualVariants gridVisualVariant;

       switch (selectedAction)
       {
        case AttackAction attackAction:
            gridVisualVariant = GridVisualVariants.Attack;

            ShowAttackRange(activeUnit.GetGridPosition(),attackAction.GetMaxAttackDistance(),attackAction.GetIsAttackCirculiar(),GridVisualVariants.AttackRange);
            break;

        default:
            gridVisualVariant = GridVisualVariants.Basic;
            break;
       }

        ShowGridPositionList(selectedAction.GetValidGridPositionList(), gridVisualVariant);
    }

    private void LevelGrid_OnUnitChangedGridPosition(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }

    private void UnitActionSystem_OnSelectedActionChange(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }

    private void Unit_OnAnyUnitDied(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }


    private Material GetGridVisualVariantsMaterial(GridVisualVariants gridVisualVariant)
    {
        foreach (GridVisualVariantsMaterial item in gridVisualVariantsMaterialList)
        {
            if (item.gridVisualVariant == gridVisualVariant)
            {
                return item.material;
            }
        }
        Debug.LogError("Material missing" + gridVisualVariant);
        return null;
    }

}
