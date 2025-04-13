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
    private playerState current_state = playerState.idle_state;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (current_state) { 
            case playerState.idle_state:
                break;
            case playerState.move_state:
                break;
            case playerState.heavy_move_state: 
                
                break;
        }
    }
}
