using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance {get; private set;}
    [SerializeField] private Transform gridSystemVisualSingleGrid;

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
    }

    private void Update()
    {
        UpdateGridVisual();
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

    public void ShowGridPositionList(List<GridPosition> gridPositionList)
    {
        foreach (GridPosition gridPosition in gridPositionList)
        {
            gridSystemVisualSingelArray[gridPosition.x, gridPosition.z].Show();
        }
    }

    private void UpdateGridVisual()
    {
        HideAllGridPosition();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        ShowGridPositionList(selectedUnit.GetMoveAction().GetValidGridPositionList());
    }

}
