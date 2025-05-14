using UnityEngine;
using System.Collections.Generic;
[DefaultExecutionOrder(-100)]

public class Grid : MonoBehaviour
{
    // object coordinate (x, y)
    public static Grid Instance;
    //Width in unity units
    public float cellWidth;
    public float cellHeight;
    //number of cells on the x and the y
    public Vector2 dimensions;
    //Info used to draw the grid and for GridObjects to place themselves
    private Vector2 _topLeft;
    private Vector2 _bottomRight;
    private GridObject myGridObject;
    public Vector2 TopLeft { get { return _topLeft; } }
    public Vector2 BottomRight { get { return _bottomRight; } }


    // store all detacted target blocks & their coordinates in lists
   // public List<Vector2Int> allPositions = new List<Vector2Int>();
    private List<GridObject> allGridObjects = new List<GridObject>();
    public List<GridObject> GetAllGridObjects() { return allGridObjects; }


    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        Instance = this;

        // 如果你想自动抓第一个方块的尺寸：
        var anyBlock = FindFirstObjectByType<GridObject>();
        if (anyBlock != null)
        {
            var sr = anyBlock.GetComponent<SpriteRenderer>();
            cellWidth = sr.bounds.size.x;
            cellHeight = sr.bounds.size.y;
        }

        Instance = this;
        _topLeft = new Vector2(-45, 30);
        _bottomRight = new Vector2(45, -30);

        myGridObject = GetComponent<GridObject>();
        
        dimensions = (_bottomRight - _topLeft) / cellWidth;
    }

    public void UpdateGridObject(GridObject gridObject)
    {
        //if not contained in the list, add
        if (!allGridObjects.Contains(gridObject))
        {
            // if not added into list, add for the first time
            allGridObjects.Add(gridObject);
            //allPositions.Add(gridObject.gridPosition);
        }
        //if contained, change position 
        else { 
            
        }
        
        
        
       
    }

   //use for restart
    private void RefrenshGrid(GridObject gridObject) 
    {
        allGridObjects.Clear();
        //allPositions.Clear();
        GridObject[] foundObjects = FindObjectsByType<GridObject>(
            FindObjectsInactive.Exclude,   // 不包括未激活的物体，按需可改成 Include
            FindObjectsSortMode.InstanceID      // 不排序，性能更高
        );

        allGridObjects.AddRange(foundObjects);


    }

    // Transform grid coordinates to world position
    public Vector3 GridToWorldPosition(Vector2Int gridPos)
    {
        float x = TopLeft.x + cellWidth * (gridPos.x - cellWidth/2);
        float y = TopLeft.y - cellWidth * (gridPos.y - cellHeight/2);
        return new Vector3(x, y, 0f);
    }
    public Vector2Int WorldToGridPosition(Vector2 worldPos)
    {
        float x = (worldPos.x - TopLeft.x) / cellWidth;
        float y = (TopLeft.y - worldPos.y) / cellHeight;
        return new Vector2Int(Mathf.RoundToInt(x), Mathf.RoundToInt(y));



    }
}



