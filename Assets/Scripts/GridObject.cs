using System;
using UnityEngine;

public class GridObject : MonoBehaviour
{


    public Vector2Int gridPosition = Vector2Int.zero;

    private Vector2Int lastPosition = Vector2Int.zero;
    private Rigidbody2D myRigidBody;
    // highlighter when selected
    private SpriteRenderer _sr;
    private Color _normalColor;
    [SerializeField] private Color _highlightColor = Color.yellow;

    public bool is_moving = false;


    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _normalColor = _sr.color;

    }

   

    void Start()
    {
        InitializeBlock();

    }

    // automatic update if coordinate changed
    void Update()
    {
        //ËæÊ±update grid cord
        Vector2Int current_grid_cord = Grid.Instance.WorldToGridPosition(transform.position);
        gridPosition = current_grid_cord;
        Debug.Log("is_moving" + is_moving);
        switch (is_moving)
        {
            case true:
                Debug.Log("is_moving turned to true");
                break;
            case false:
                Debug.Log("is_moving turned to false");

                //if (current_grid_cord != lastPosition)
                //{
                GridSnap();                   
                   // gridPosition = current_grid_cord;
                    //last position should be updated only when changed to idle;
                   // lastPosition = current_grid_cord;
                    Grid.Instance.UpdateGridObject(this);
               // }
             
            break;
        }
    }
    private void InitializeBlock()
    {
        Vector2 myWorldPos = transform.position;
        gridPosition = Grid.Instance.WorldToGridPosition(myWorldPos);
        Grid.Instance.UpdateGridObject(this);
        

        myWorldPos = Grid.Instance.GridToWorldPosition(gridPosition);
       
    }

    //highlight change state

    public void Highlight(bool on)
    {
        if (_sr == null) _sr = GetComponent<SpriteRenderer>();
        _sr.color = on ? _highlightColor : _normalColor;
    }

    // if coordinate changed or updated£¬call this function to tell GridManager
    public void SetGridPosition(int x, int y)
    {
        gridPosition = new Vector2Int(x, y);
        Grid.Instance.UpdateGridObject(this);
    }
    public void GridSnap()
    {
        //transform coordinat to grid coordinates       
        Vector2Int _newGridPos = Grid.Instance.WorldToGridPosition(transform.position);
        
        //transform gridpos to worldpos
        Vector3 _newWorldPos = Grid.Instance.GridToWorldPosition(_newGridPos);

        transform.position = _newWorldPos;
        Debug.Log("GridSnap() triggered");
    }
}



