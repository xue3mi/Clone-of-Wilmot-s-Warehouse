using JetBrains.Annotations;
using UnityEngine;
[RequireComponent(typeof(GridObject))]
public class PlayerMovement : MonoBehaviour
{
    //工具类
   

    private GridObject _gridObj;
    private Vector2 _targetWorldPos;
    private bool _isMoving;
    private Player _player;
    private int dir = 0;

    private void Awake()
    {
        _gridObj = GetComponent<GridObject>();
        _player = GetComponent<Player>();
        // 初始吸附到当前格的中心
    }

    private void Start()
    {

        //_targetWorldPos = Grid.Instance.GridToWorldPosition(_gridObj.gridPosition);
        //transform.position = _targetWorldPos;

    }

    private void Update()
    {

        /*if (!_isMoving)
            TryStartMove();
        else
            ContinueMove();*/
    }


    /*
    private void TryStartMove()
    {
        // 只检查按键，不做其他向量运算
        if (Input.GetKeyDown(KeyCode.W)) BeginMove(Vector2Int.up);
        else if (Input.GetKeyDown(KeyCode.S)) BeginMove(Vector2Int.down);
        else if (Input.GetKeyDown(KeyCode.A)) BeginMove(Vector2Int.left);
        else if (Input.GetKeyDown(KeyCode.D)) BeginMove(Vector2Int.right);
    }*/


    
    //keep updating to override the previous dir, to ensure theres only 4 dir
    public int CheckMoveDirection()
    {
   
        if (Input.GetKeyDown(KeyCode.W)) dir = 1;
        else if (Input.GetKeyDown(KeyCode.S)) dir = 2;
        else if (Input.GetKeyDown(KeyCode.A)) dir = 3;
        else if (Input.GetKeyDown(KeyCode.D)) dir = 4;
  

        if (!IsInputMove()){ dir = 0; }
        //Debug.Log($"CheckMoveDirection → dir = {dir}");
        return dir;
    }

    //check if is inputing movement
    public static bool IsInputMove() {
        if (Input.GetKey(KeyCode.W)
        || Input.GetKey(KeyCode.S) 
        ||Input.GetKey(KeyCode.A)
        || Input.GetKey(KeyCode.D))
        { 
            return true; 
        }
        else { return false; }
    }

    public static void Move(ref Vector2 currentVel, Vector2 inputDir, 
        float speed, float smoothTime, ref Vector2 velocityRef, Transform transform)
    {
        // 1. 计算目标速度
        Vector2 targetVel = inputDir * speed;
        // 2. 平滑过渡，更新 currentVel
        currentVel = Vector2.SmoothDamp(currentVel, targetVel, ref velocityRef, smoothTime);
        // 3. 根据 newVel 更新位置（注意乘以 Time.deltaTime）
        transform.position += (Vector3)(currentVel * Time.deltaTime);
    }

    private void BeginMove(Vector2Int dir)  
    {
        // 标记正在移动，阻止 GridObject 自动吸附
        _gridObj.is_moving = true;
        _isMoving = true;

        // 1) 更新格坐标
        Vector2Int nextGrid = _gridObj.gridPosition + dir;
        _gridObj.SetGridPosition(nextGrid.x, nextGrid.y);

        // 2) 计算目标世界坐标
        Vector3 wp = Grid.Instance.GridToWorldPosition(nextGrid);
        _targetWorldPos = new Vector2(wp.x, wp.y);
    }

   /* private void ContinueMove()
    {
        // 平滑移动
        Vector2 cur = transform.position;
        Vector2 next = Vector2.SmoothDamp(cur, _targetWorldPos, ref _velocity, smoothTime);
        transform.position = next;

        // 到达本格中心后
        if ((next - _targetWorldPos).sqrMagnitude < 0.0001f)
        {
            // 强制对齐
            transform.position = _targetWorldPos;

            // 松开所有方向键才结束
            bool holding = Input.GetKey(KeyCode.W)
                         || Input.GetKey(KeyCode.S)
                         || Input.GetKey(KeyCode.A)
                         || Input.GetKey(KeyCode.D);

            if (holding)
            {
                // 还有按键：准备下一格
                _isMoving = false;
            }
            else
            {
                // 松手：吸附回格点、恢复状态
                _gridObj.GridSnap();
                _gridObj.is_moving = false;
                _isMoving = false;
                _player.current_state = Player.playerState.idle_state;
            }
        }
    }*/
}
