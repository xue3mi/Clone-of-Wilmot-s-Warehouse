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
    private bool _wasMoving = true;
    public bool is_gridSnapped = false;


    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _normalColor = _sr.color;

    }

   

    void Start()
    {
        InitializeBlock();
        _wasMoving = is_moving;
    }

    // automatic update if coordinate changed
    void Update()
    {
        //随时update grid cord
        Vector2Int current_grid_cord = Grid.Instance.WorldToGridPosition(transform.position);
        gridPosition = current_grid_cord;
        Debug.Log("is_moving" + is_moving);

        // 检测从 moving → idle 的瞬间
        if (_wasMoving && !is_moving)
        {
            // 只在状态切换的这一帧执行一次
            GridSnap();
            Debug.Log("GridSnap() triggered on stop moving");
        }
        Grid.Instance.UpdateGridObject(this);

        // 最后再把当前状态存到 _wasMoving，供下一帧比较
        _wasMoving = is_moving;

        switch (is_moving)
        {
            case true:
               // is_gridSnapped = false;
                Debug.Log("is_moving turned to true");
                break;
            case false:
                Debug.Log("is_moving turned to false");
                //is_gridSnapped = true;
                if (is_gridSnapped)
                {
                    GridSnap();
                   // is_gridSnapped=false;


                }

                //if (current_grid_cord != lastPosition)
                //{
                // gridPosition = current_grid_cord;
                //last position should be updated only when changed to idle;
                // lastPosition = current_grid_cord;
               
               // }
             
            break;
        }
    }
    private void InitializeBlock()
    {
        Vector2 worldPos = transform.position;
        gridPosition = Grid.Instance.WorldToGridPosition(worldPos);
        Grid.Instance.UpdateGridObject(this);

        // 2. 再把格子坐标转回“格子中心”的世界坐标
        Vector3 snappedPos = Grid.Instance.GridToWorldPosition(gridPosition);

        // 3. **真正把物体瞬移到格子中心**  
        transform.position = snappedPos;

    }

    //highlight change state

    public void Highlight(bool on)
    {
        if (_sr == null) _sr = GetComponent<SpriteRenderer>();
        _sr.color = on ? _highlightColor : _normalColor;
    }

    // if coordinate changed or updated，call this function to tell GridManager
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



