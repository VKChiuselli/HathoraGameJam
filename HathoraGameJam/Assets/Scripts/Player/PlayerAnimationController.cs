using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public float movementThresholdAbove = 0.0055f;
    public float movementThresholdBelow = 0.0008f;
    public int counterThreshold = 5;

    private Vector3 lastPosition;
    public int movementCounter = 0;
    public Animator playerAnimator;
    bool playerCaught;
    private void Start()
    {
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 currentPosition = transform.position;
        float distanceMoved = Vector3.Distance(currentPosition, lastPosition);
        //Debug.Log("distanceMoved: " + distanceMoved);
        if (distanceMoved > movementThresholdAbove)
        {
            CallFunctionOnThresholdReached(distanceMoved);
        }
        else  
        {
            CallFunctionOnThresholdReached(distanceMoved);
        }
       

       

        lastPosition = currentPosition;
    }

    private void CallFunctionOnThresholdReached(float DISTANCE)
    {
        // Call your function when the counter goes above the threshold
        Debug.Log("Threshold reached!");
        // YourFunctionToCallOnThresholdReached(); 
        GetComponent<AudioSource>().volume =  DISTANCE*10f;
        playerAnimator.SetFloat("Speed", DISTANCE);
    }

    private void CallFunctionOnThresholdDecreased()
    {
        // Call your function when the counter goes below the negative threshold
        Debug.Log("Threshold decreased!");
       GetComponent<SFX>().StopFirstEffect();
        // YourFunctionToCallOnThresholdDecreased();
    }
}
