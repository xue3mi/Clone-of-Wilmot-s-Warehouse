using UnityEngine;
using System.Collections.Generic;

/*public class Grid: MonoBehaviour
{
    public static List<GridObject> gridObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/

using UnityEngine;

public class Grid : MonoBehaviour
{
    // object coordinate (x, y)
    public static Grid Instance;

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
        }
        // Every update, refresh the coordinate list
        RefreshAllPositions();
    }

    //gridobj reference
    private void RefrenshGrid() 
    {
        allGridObjects.Clear();
        allPositions.Clear();
        GridObject[] gridObjs = FindObjectOfType<GridObject>();

    
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
            Debug.Log("All blocks' coordinate£º");
            foreach (Vector2Int pos in allPositions)
            {
                Debug.Log(pos);
            }
        }
    }


}

    
