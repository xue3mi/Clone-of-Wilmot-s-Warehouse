using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float characterSpeed = 5.0f; // Speed of Wilmot

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //detect input, movementInput can be used for animation later
        Vector2 movementInput = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += new Vector3(0, characterSpeed * Time.deltaTime, 0);
            movementInput.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position += new Vector3(0, -characterSpeed * Time.deltaTime, 0);
            movementInput.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += new Vector3(-characterSpeed * Time.deltaTime, 0, 0);
            movementInput.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += new Vector3(characterSpeed * Time.deltaTime, 0, 0);
            movementInput.x = 1;
        }

        // to be add: If the player is moving, play the walking animation
    }
}
