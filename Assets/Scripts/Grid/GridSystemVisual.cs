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
                GridPosition gridPosition = new(x,z);
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

    private void ShowRange(GridPosition unitGridPosition, int range, bool isCirculiar, GridVisualVariants gridVisualVariant)
    {
        List<GridPosition> validGridPositionList = new();

        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                GridPosition offsetGridPosition = new(x,z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if(!LevelGrid.Instance.IsValidPosition(testGridPosition))
                {
                    continue;
                }

                if (isCirculiar)
                {
                    int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                    if (testDistance > range)
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

            ShowRange(activeUnit.GetGridPosition(), 
                    attackAction.GetRange(), 
                    attackAction.GetIsCirculiar(), 
                    GridVisualVariants.AttackRange);
            break;

        case SummonAction:
            gridVisualVariant = GridVisualVariants.Summon;
            break;

        case SupportAction supportAction:
            gridVisualVariant = GridVisualVariants.Support;
            ShowRange(activeUnit.GetGridPosition(),
                        supportAction.GetRange(),
                        supportAction.GetIsCirculiar(),
                        GridVisualVariants.SupportRange );
            break;

        case AntiSupportAction antiSupportAction:
            gridVisualVariant = GridVisualVariants.Debuff;
            ShowRange(activeUnit.GetGridPosition(),
                        antiSupportAction.GetRange(),
                        antiSupportAction.GetIsCirculiar(),
                        GridVisualVariants.DebuffRange);
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
