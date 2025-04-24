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
using Unity.VisualScripting;

public class Grid : MonoBehaviour
{
    // object coordinate (x, y)
    public static Grid Instance;
    public float cellWidth = 64f; //Width in unity units
    public Vector2 dimensions; //number of cells on the x and the y


    //Info used to draw the grid and for GridObjects to place themselves
    private Vector2 _topLeft;
    private Vector2 _bottomRight;
    public Vector2 TopLeft { get { return _topLeft; } }
    public Vector2 BottomRight { get { return _bottomRight; } }


    // store all detacted target blocks & their coordinates in lists
    public List<Vector2Int> allPositions = new List<Vector2Int>();
    private List<GridObject> allGridObjects = new List<GridObject>();

    // demonstration. Not for production
    public GameStateManager theStateManagerFromThisObject;

    // demonstration
    
    private void Start()
    {
        theStateManagerFromThisObject = this.GetComponent<GameStateManager>(); // something like this actually returns the whole game object, instead of just the component.
        LightManager manager = theStateManagerFromThisObject.GetComponent<LightManager>(); // because this is the whole game object, you can access other scripts under it.

        GridObject[] objs = FindObjectsByType<GridObject>(FindObjectsSortMode.None);//这个obj 找到的是 所有 具有gridobject这个script的 object
        objs[0].GetComponent<Player>();
    }
    

    void Awake() 
    { 
        Instance = this;
    }


    public List<GridObject> GetAllGridObjects() { return allGridObjects; }
    public List<Vector2Int> GetAllPositions() { return allPositions;}


    public void UpdateGridObject(GridObject gridObject)
    {
        if (!allGridObjects.Contains(gridObject))
        {
            // if not added into list, add for the first time
            allGridObjects.Add(gridObject);
            allPositions.Add(gridObject.gridPosition);
        }


    }

    


    //gridobj reference
    private void RefreshGrid(GridObject gridObject) 
    {
        allGridObjects.Clear();
     
        //add all gridobj on the scene. needs TESTINg
        GridObject[] foundObjects = FindObjectsByType<GridObject>(
            FindObjectsInactive.Exclude,   // 不包括未激活的物体，按需可改成 Include
            FindObjectsSortMode.InstanceID      // 不排序，性能更高
        );

        allGridObjects.AddRange(foundObjects);
        RefreshAllPositions(); 


        //initioalize grid dimentions
        _topLeft.x = this.transform.position.x - (dimensions.x * cellWidth * 0.5f);
        _topLeft.y = this.transform.position.y + (dimensions.y * cellWidth * 0.5f);

        _bottomRight = Vector2.zero;
        _bottomRight.x = this.transform.position.x + (dimensions.x * cellWidth * 0.5f);
        _bottomRight.y = this.transform.position.y - (dimensions.y * cellWidth * 0.5f);




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

    /*
    private void GridSnap()
    {
        //transform coordinat to grid coordinates


        // set position & keep Z axis

        float x = Grid.Instance.TopLeft.x + Grid.Instance.cellWidth * (gridPosition.x - 0.5f);
        float y = Grid.Instance.TopLeft.y - Grid.Instance.cellWidth * (gridPosition.y - 0.5f);
        transform.position = new Vector3(x, y, transform.position.z);
        // object coordinate (x, y)
    }
    */



    /*
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
    test*/


}

 
