using UnityEngine;

public class GridSystem
{

    private int width;
    private int length;
    private float cellSize;
    private GridObject[,] gridObjectsArray;

        public GridSystem(int width, int length, float cellSize )
    {
        this.width = width;
        this.length = length;
        this.cellSize = cellSize;

        gridObjectsArray = new GridObject[width,length];
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                gridObjectsArray[x,z] = new GridObject(this, gridPosition);
            }
            
        }

    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z)* cellSize;
    }

     public GridPosition GetGridPosition(Vector3 worldPosition)
    {
     return new GridPosition(
        Mathf.RoundToInt(worldPosition.x / cellSize),
        Mathf.RoundToInt(worldPosition.z / cellSize)
     );   
    }

    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                Transform debugTransform = GameObject.Instantiate(debugPrefab,GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject= debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectsArray[gridPosition.x, gridPosition.z];
    }

/*  */
}
