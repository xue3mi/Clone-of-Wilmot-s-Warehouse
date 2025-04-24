using UnityEngine;

public class GridObject : MonoBehaviour
{

    //public Vector2 gridPositio = Vector2.zero;
    public Vector2Int gridPosition;
    private Vector2Int lastPosition;

    private enum MovingState
    { 
        Idle,
        Moving
        
    }
    private MovingState my_moving_state = MovingState.Idle;

    void Start()
    {
        //calculate world position(coordinate) according to its own position
        Vector2 myWorldPos = transform.position;
        //gridPosition = new Vector2Int(Mathf.RoundToInt(myWorldPos.x), Mathf.RoundToInt(myWorldPos.y));
        lastPosition = gridPosition;

        // tell GridManager coordinate
        Grid.Instance.UpdateGridObject(this);
    }

    // automatic update if coordinate changed
    void Update()
    {
        //if not connected to wilmot && moving
        //GridSnap()

        switch (my_moving_state) 
        {
            case MovingState.Idle:

                break;
            case MovingState.Moving:
                //GridSnap();
                Grid.Instance.UpdateGridObject(this);

                break;
        }

    }

    // if coordinate changed or updated£¬call this function to tell GridManager
    public void SetGridPosition(int x, int y)
    {
        gridPosition = new Vector2Int(x, y);
        Grid.Instance.UpdateGridObject(this);
    }
}



