using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class Player : MonoBehaviour
{
    //manager类

    //constants
    public const float normal_speed = 5f;
    public const float heavy_speed = 1f;


    public enum playerState { idle_state, move_state, heavy_move_state }
    
    public playerState current_state = playerState.idle_state;
    private PlayerMovement myPlayerMovement;
    private GridObject myGridObject;
    private Rigidbody2D myRigidbody;
    private bool can_select = false;
    private float _speed = normal_speed;
    private Vector2 _dir;
    private Vector2 _velocity;
    private Vector2 current_velocity;
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
                myGridObject.is_moving = false;
                HandleIdle();
                break;
            case playerState.move_state:
                myGridObject.is_moving = true;
                HandleMove();
                break;
            case playerState.heavy_move_state:
                myGridObject.is_moving = true;

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
        _speed = 0;
        _dir = Vector2.zero;
        _velocity = Vector2.zero;

        //myGridObject.GridSnap();//////這個gridobj裡有ismoving調控

        // 检测开始移动，change state
            //只有先停下到idle_state才能转换到其他两个movestate
        if (PlayerMovement.IsInputMove())
        {
            //检测selection set的数量决定changestate到另外两个*未写完
            current_state = playerState.move_state;
            //Debug.Log("State set to move");
        }
    }

    private void HandleMove()
    {
        _speed = normal_speed;
        can_select = false;
        // 连续调用移动逻辑
        //已经按下wasd，那么根据方向移动，然后平滑
        
        ChangeMoveDirection();
        PlayerMovement.Move(ref _velocity, _dir, _speed, _smoothTime, ref current_velocity, transform);

        Debug.Log($"HandleMove → _velocity = {_velocity}");
        //state change conditions
        //if (_velocity == Vector2.zero) {
        if (!PlayerMovement.IsInputMove()) {

           current_state = playerState.idle_state;
           // Debug.Log("State set to idle");
        }

      
    }

    private void ChangeMoveDirection() {

        int pressed = myPlayerMovement.CheckMoveDirection();
        Debug.Log($"ChangeMoveDirection → pressed = {pressed}");

        switch (pressed)
        {
            case 1: current_move_dir = _moveDir.up;    break;
            case 2: current_move_dir = _moveDir.down;  break;
            case 3: current_move_dir = _moveDir.left;  break;
            case 4: current_move_dir = _moveDir.right; break;
            case 0: current_move_dir = _moveDir.none; break;

        }
        Debug.Log($"ChangeMoveDirection → current_move_dir = {current_move_dir}");

        switch (current_move_dir)
    {
        case _moveDir.up:    _dir = Vector2.up;    break;
        case _moveDir.down:  _dir = Vector2.down;  break;
        case _moveDir.left:  _dir = Vector2.left;  break;
        case _moveDir.right: _dir = Vector2.right; break;
        case _moveDir.none:  _dir = Vector2.zero;  break;
    }
        Debug.Log($"ChangeMoveDirection → _dir = {_dir}");
    }

    
    private void HandleHeavyMove()
    {
        _speed = heavy_speed;
        // …你已有的重移动逻辑…
    }
}
