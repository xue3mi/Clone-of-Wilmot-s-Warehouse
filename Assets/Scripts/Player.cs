using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private enum playerState 
    {
        idle_state,
        move_state,
        heavy_move_state
    }
    private playerState current_state = playerState.move_state;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (current_state) { 
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

    private void HandleHeavyMove()
    {
        throw new NotImplementedException();
    }

    private void HandleMove()
    {
   
        PlayerMovement myPlayerMovement = GetComponent<PlayerMovement>();
        myPlayerMovement.MovePlayer();
        throw new NotImplementedException();
    }

    private void HandleIdle()
    {
        GridObject myGridObject = GetComponent<GridObject>();
        myGridObject.GridSnap();

    }
}
