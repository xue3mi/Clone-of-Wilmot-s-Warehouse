using UnityEngine;
using System.Collections.Generic;


public class Grid : MonoBehaviour
{
    // object coordinate (x, y)
    public static Grid Instance;
    //Width in unity units
    public float cellWidth = 64f;
    //number of cells on the x and the y
    public Vector2 dimensions;
    //Info used to draw the grid and for GridObjects to place themselves
    private Vector2 _topLeft;
    private Vector2 _bottomRight;
    public Vector2 TopLeft { get { return _topLeft; } }
    public Vector2 BottomRight { get { return _bottomRight; } }


    // store all detacted target blocks & their coordinates in lists
    public List<Vector2Int> allPositions = new List<Vector2Int>();
    private List<GridObject> allGridObjects = new List<GridObject>();

    void Awake()
    {
        Instance = this;
    }

    public void UpdateGridObject(GridObject gridObject)
    {
        if (!allGridObjects.Contains(gridObject))
        {
            // if not added into list, add for the first time
            allGridObjects.Add(gridObject);
            allPositions.Add(gridObject.gridPosition);
        }
        
       
    }

    //use for lightning
    public List<GridObject> GetAllGridObjects()
    {
        return allGridObjects;
    }




    //gridobj reference
    private void RefrenshGrid(GridObject gridObject) 
    {
        allGridObjects.Clear();
        allPositions.Clear();
        GridObject[] foundObjects = FindObjectsByType<GridObject>(
            FindObjectsInactive.Exclude,   // 不包括未激活的物体，按需可改成 Include
            FindObjectsSortMode.InstanceID      // 不排序，性能更高
        );

        allGridObjects.AddRange(foundObjects);


    }

    void RefreshAllPositions()
    {
        // delete old coordinates if updated
        allPositions.Clear();

        foreach (GridObject obj in allGridObjects)
        {
            // add the newest coordinates to list
            allPositions.Add(obj.gridPosition);
        }
    }

    // Transform grid coordinates to world position
    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x * cellWidth, gridPosition.y * cellWidth, 0f);
    }


    // test
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // Every sec update the coordinates
        if (timer >= 1f)
        {
            timer = 0f;

            // print all coordinates positions
            Debug.Log("All blocks' coordinate：");
            foreach (Vector2Int pos in allPositions)
            {
                Debug.Log(pos);
            }
        }
    }


}

 
