using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCutsceneManager: MonoBehaviour
{
    public Transform player;
    public Transform upperFocusPoint;
    public Transform lowerFocusPoint;

    public PlayerMovement playerMovement;
    public CameraFollow cameraFollow;
    
    //truck animation
    public Transform truck;
    public float truckMoveSpeed = 2f;
    public float blockSize = 1f;
    public float ignoreLightingDuration = 10f;

    public List<GameObject> customerBubbles;
    public GameObject blocksIgnoreParent;

    public float moveSpeed = 50f;
    // 2m 41s release animation
    public float waitTimeAtTarget = 5f;

    void Start()
    {
        // initial position of truck
        truck.position = new Vector3(0, -21, 0);
    }


    public IEnumerator LowerCameraMove()
    {
        DisablePlayerControl();
        yield return StartCoroutine(MoveCamera(lowerFocusPoint.position));

        //set ignoreLight before animation;; truck animation start after 3s
        SetTilesToIgnoreLighting();
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(MoveTruckDown());
        //switch when 2m 45s
        yield return new WaitForSeconds(waitTimeAtTarget);
        yield return StartCoroutine(MoveCamera(player.position));
        EnablePlayerControl();
    }

    public IEnumerator UpperCameraMove()
    {
        DisablePlayerControl();
        yield return StartCoroutine(MoveCamera(upperFocusPoint.position));
        //5s animation
        yield return new WaitForSeconds(waitTimeAtTarget);
        yield return StartCoroutine(MoveCamera(player.position));
        EnablePlayerControl();

        //show customerBubbles after UpperAnimation played for 1s
        yield return new WaitForSeconds(1f);
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

    private void SetTilesToIgnoreLighting()
    {
        foreach (Transform child in blocksIgnoreParent.transform)
        {
            LightningTile tile = child.GetComponent<LightningTile>();
            if (tile != null)
            {
                tile.SetIgnoreLighting(ignoreLightingDuration);
            }
        }
    }

    private IEnumerator MoveTruckDown()
    {
        Vector3 startPos = truck.position;
        Vector3 endPos = startPos + new Vector3(0, -5 * blockSize, 0);

        while (Vector3.Distance(truck.position, endPos) > 0.01f)
        {
            truck.position = Vector3.MoveTowards(truck.position, endPos, truckMoveSpeed * Time.deltaTime);
            yield return null;
        }
    }


}
