using UnityEngine;

public class GridObject: MonoBehaviour
{

    public Vector2 gridPositio = Vector2.zero;
 
   
    

    
    public Vector2Int gridPosition;

    private Vector2Int lastPosition;

    void Start()
    {
        //calculate world position(coordinate) according to its own position
        Vector2 myWorldPos = transform.position;
        gridPosition = new Vector2Int(Mathf.RoundToInt(myWorldPos.x), Mathf.RoundToInt(myWorldPos.y));
        lastPosition = gridPosition;

        // tell GridManager coordinate
        Grid.Instance.UpdateGridObject(this);
    }

    // automatic update if coordinate changed
    void Update()
    {
        //if not connected to wilmot && moving
        //GridSnap()



        Vector2Int currentPosition = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        if (currentPosition != lastPosition)
        {
            gridPosition = currentPosition;
            lastPosition = currentPosition;
            Grid.Instance.UpdateGridObject(this);
        }
    }

    // if coordinate changed or updated£¬call this function to tell GridManager
    public void SetGridPosition(int x, int y)
    {
        gridPosition = new Vector2Int(x, y);
        Grid.Instance.UpdateGridObject(this);
    }
    private void GridSnap()
    {
        //transform coordinat to grid coordinates
        // round to int & align the coordinate
        Vector2Int snappedPos = new Vector2Int(
            // change math CALCULATION
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y)
        );

        // set position & keep Z axis
        transform.position = new Vector3(snappedPos.x, snappedPos.y, transform.position.z);
    }
    // object coordinate (x, y)
}



