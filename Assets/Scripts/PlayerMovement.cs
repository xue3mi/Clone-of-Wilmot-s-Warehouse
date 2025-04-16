using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float characterSpeed;
  

    public void Move(Vector2 inputDirection)
    {
        Vector3 movement = new Vector3(inputDirection.x, inputDirection.y, 0f) * characterSpeed * Time.deltaTime;
        transform.position += movement;
    }


    public Vector2 GetMovementInput() 
    {
        Vector2 movementInput = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            movementInput.y += 1;
        if (Input.GetKey(KeyCode.S))
            movementInput.y -= 1;
        if (Input.GetKey(KeyCode.A))
            movementInput.x -= 1;
        if (Input.GetKey(KeyCode.D))
            movementInput.x += 1;
        return movementInput.normalized;
    }

    

    public bool CheckKeyRelease()
    {
        return Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) ||
               Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D);
    }
}
