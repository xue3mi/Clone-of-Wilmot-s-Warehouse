using System.Collections;
using UnityEngine;

public class StartSceneCameraMovement : MonoBehaviour
{
    public Transform player;
    public Transform upperFocusPoint;
    public Transform lowerFocusPoint;
    // get animation
    // public Animator upperSceneAnimation;
    // public Animator lowerSceneAnimation;
    public PlayerMovement playerMovement;
    public CameraFollow cameraFollow;

    // camera move speed
    public float moveSpeed = 5f;
    // stays at upperScene for x seconds
    public float waitTimeAtTarget = 5f;

    private void Start()
    {
        // disable player control while intro is playing
        playerMovement.enabled = false;
        cameraFollow.enabled = false;

        // Start startScene animation
        StartCoroutine(PlayIntroSequence());
    }

    private IEnumerator PlayIntroSequence()
    {
        // Step 1: camera move up to UpperFocusPoint
        yield return StartCoroutine(MoveCamera(upperFocusPoint.position));

        // Step 2: Play upperScene animation 
        // animationSpriteAnimator.SetTrigger("Play");
        // wait until animation finishes
        // yield return new WaitForSeconds(animationSpriteAnimator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(waitTimeAtTarget);

        // Step 3: camera move down to LowerFocusPoint
        yield return StartCoroutine(MoveCamera(lowerFocusPoint.position));

        // Step 2: Play lowerScene animation 
        // animationSpriteAnimator.SetTrigger("Play");
        // wait until animation finishes
        // yield return new WaitForSeconds(animationSpriteAnimator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(waitTimeAtTarget);

        // Step 5: camera back to player position
        yield return StartCoroutine(MoveCamera(player.position));

        // Step 6: enable player control & camera follow
        cameraFollow.enabled = true;
        playerMovement.enabled = true;
    }

    private IEnumerator MoveCamera(Vector3 targetPosition)
    {
        // keep camera z-axis position
        targetPosition.z = transform.position.z;
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
