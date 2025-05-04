using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCutsceneManager : MonoBehaviour
{
    public Transform player;
    public Transform upperFocusPoint;
    public Transform lowerFocusPoint;

    public PlayerMovement playerMovement;
    public CameraFollow cameraFollow;

    public List<GameObject> customerBubbles;

    public float moveSpeed = 50f;
    // 2m 41s release animation
    public float waitTimeAtTarget = 10f;

    public IEnumerator LowerCameraMove()
    {
        DisablePlayerControl();
        yield return StartCoroutine(MoveCamera(lowerFocusPoint.position));
        yield return new WaitForSeconds(waitTimeAtTarget);
        yield return StartCoroutine(MoveCamera(player.position));
        EnablePlayerControl();
    }

    public IEnumerator UpperCameraMove()
    {
        DisablePlayerControl();
        yield return StartCoroutine(MoveCamera(upperFocusPoint.position));
        yield return new WaitForSeconds(waitTimeAtTarget);
        yield return StartCoroutine(MoveCamera(player.position));
        EnablePlayerControl();

        //show customerBubbles after UpperAnimation played for 3s
        yield return new WaitForSeconds(3f);
        foreach (GameObject bubble in customerBubbles)
        {
            if (bubble != null)
            {
                bubble.SetActive(true);
            }
        }

        EnablePlayerControl();
    }

    private IEnumerator MoveCamera(Vector3 targetPosition)
    {
        targetPosition.z = transform.position.z;
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void DisablePlayerControl()
    {
        playerMovement.enabled = false;
        cameraFollow.enabled = false;
    }

    private void EnablePlayerControl()
    {
        cameraFollow.enabled = true;
        playerMovement.enabled = true;
    }
}
