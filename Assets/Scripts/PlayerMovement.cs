using JetBrains.Annotations;
using UnityEngine;
[RequireComponent(typeof(GridObject))]
public class PlayerMovement : MonoBehaviour
{
    //������
   

    private GridObject _gridObj;
    private Vector2 _targetWorldPos;
    private bool _isMoving;
    private Player _player;
    private int dir = 0;

    private void Awake()
    {
        _gridObj = GetComponent<GridObject>();
        _player = GetComponent<Player>();
        // ��ʼ��������ǰ�������
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
        // ֻ��鰴��������������������
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
        //Debug.Log($"CheckMoveDirection �� dir = {dir}");
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
        // 1. ����Ŀ���ٶ�
        Vector2 targetVel = inputDir * speed;
        // 2. ƽ�����ɣ����� currentVel
        currentVel = Vector2.SmoothDamp(currentVel, targetVel, ref velocityRef, smoothTime);
        // 3. ���� newVel ����λ�ã�ע����� Time.deltaTime��
        transform.position += (Vector3)(currentVel * Time.deltaTime);
    }

    private void BeginMove(Vector2Int dir)  
    {
        // ��������ƶ�����ֹ GridObject �Զ�����
        _gridObj.is_moving = true;
        _isMoving = true;

        // 1) ���¸�����
        Vector2Int nextGrid = _gridObj.gridPosition + dir;
        _gridObj.SetGridPosition(nextGrid.x, nextGrid.y);

        // 2) ����Ŀ����������
        Vector3 wp = Grid.Instance.GridToWorldPosition(nextGrid);
        _targetWorldPos = new Vector2(wp.x, wp.y);
    }

   /* private void ContinueMove()
    {
        // ƽ���ƶ�
        Vector2 cur = transform.position;
        Vector2 next = Vector2.SmoothDamp(cur, _targetWorldPos, ref _velocity, smoothTime);
        transform.position = next;

        // ���ﱾ�����ĺ�
        if ((next - _targetWorldPos).sqrMagnitude < 0.0001f)
        {
            // ǿ�ƶ���
            transform.position = _targetWorldPos;

            // �ɿ����з�����Ž���
            bool holding = Input.GetKey(KeyCode.W)
                         || Input.GetKey(KeyCode.S)
                         || Input.GetKey(KeyCode.A)
                         || Input.GetKey(KeyCode.D);

            if (holding)
            {
                // ���а�����׼����һ��
                _isMoving = false;
            }
            else
            {
                // ���֣������ظ�㡢�ָ�״̬
                _gridObj.GridSnap();
                _gridObj.is_moving = false;
                _isMoving = false;
                _player.current_state = Player.playerState.idle_state;
            }
        }
    }*/
}
