using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class Player : MonoBehaviour
{
    //manager类

    //constants
    public const float normal_speed = 50f;
    public const float heavy_speed = 10f;


    public enum playerState { idle_state, move_state, heavy_move_state }
    
    public playerState current_state = playerState.idle_state;
    private PlayerMovement myPlayerMovement;
    private GridObject myGridObject;
    private Rigidbody2D myRigidbody;
    private bool can_select = false;
    private float _speed = normal_speed;
    private Vector2 _dir;
    private Vector2 _velocity;

    //smoothdamp
    private float _smoothTime = 0.1f;
    
    //use eum to limit direction to only four
    private enum _moveDir
    {
        up, down, left, right, none
    }
    private _moveDir current_move_dir = _moveDir.none;



    private void Start()
    {
        myPlayerMovement = GetComponent<PlayerMovement>();
        myGridObject = GetComponent<GridObject>();
        myRigidbody = GetComponent<Rigidbody2D>();
        

}
void Update()
    {

        switch (current_state)
        {
            case playerState.idle_state:
                HandleIdle();
                break;
            case playerState.move_state:
                HandleMove();
                break;
            case playerState.heavy_move_state:
                HandleHeavyMove();
                break;
        }
    }

    // get setter
    // playerMovement.SetDirection(new Vector2(1, 0));
    public void SetDirection(Vector2 dir)
    {
        _dir = dir.normalized;
    }
    private void HandleIdle()
    {
        
        can_select = true;
        //var mover = GetComponent<PlayerMovement>();
       // mover.enabled = false;
        // Idle 时锁定在格点
       // myGridObject.GridSnap();

        // 检测开始移动，change state
            //只有先停下到idle_state才能转换到其他两个movestate
        if (myPlayerMovement.IsInputMove())
        {
            //检测selection set的数量决定changestate到另外两个*未写完
            current_state = playerState.move_state;
        }
    }

    private void HandleMove()
    {
        _speed = normal_speed;
        can_select = false;
        // 连续调用移动逻辑
        //已经按下wasd，那么根据方向移动，然后平滑
        
        ChangeMoveDirection();
        PlayerMovement.Move(myRigidbody, _dir, _speed, _smoothTime, ref _velocity);


        //state change conditions
        if (_velocity == Vector2.zero) {
            current_state = playerState.idle_state;
        }

      
    }

    private void ChangeMoveDirection() {
        switch (myPlayerMovement.CheckMoveDirection())
        {
            case 0:
                current_move_dir = _moveDir.none;
                break;
            case 1:
                current_move_dir = _moveDir.up;
                break;
            case 2:
                current_move_dir = _moveDir.down;
                break;
            case 3:
                current_move_dir = _moveDir.left;

                break;
            case 4:
                current_move_dir = _moveDir.right;

                break;
                
        }
        switch (current_move_dir)
        {
            case _moveDir.up:
                _dir = Vector2.up;
                break;
            case _moveDir.down:
                _dir = Vector2.down;

                break;
            case _moveDir.left:
                _dir = Vector2.left;

                break;
            case _moveDir.right:
                _dir = Vector2.right;

                break;
            case _moveDir.none:
                _dir = Vector2.zero;

                break;


        }
    }
    
    private void HandleHeavyMove()
    {
        _speed = heavy_speed;
        // …你已有的重移动逻辑…
    }
}
