using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // set z to -10 for camera

    public Transform target; // transform player
    public float smoothSpeed = 0.125f; // smooth speed
    public Vector3 offset; // camera & player offset

    void LateUpdate()
    {
        // Check if target(player) exist & is assigned
        if (target != null)
        {
            //considering offset, calculate desired position
            Vector3 desiredPosition = target.position + offset;
            //smooth movement: currentPos / targetPos / speed
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // move camera to the calculated position
            transform.position = smoothedPosition;
        }
    }
}
