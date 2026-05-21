using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GridSystem
{

    private readonly int width;
    private readonly int length;
    private readonly float cellSize;
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
                GridPosition gridPosition = new(x,z);
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

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectsArray[gridPosition.x, gridPosition.z];
    }

    public bool IsVaidGridPositiono(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 &&
                gridPosition.z >= 0 && 
                gridPosition.x < width && 
                gridPosition.z < length;
    }

    public int GetWidth()
    {
        return width;
    }
    public int GetLength()
    {
        return length;
    }


}
