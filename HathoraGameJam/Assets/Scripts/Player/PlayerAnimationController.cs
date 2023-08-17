using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public float movementThresholdAbove = 0.0055f;
    public float movementThresholdBelow = 0.0018f;
    public int counterThreshold = 5;

    private Vector3 lastPosition;
    public int movementCounter = 0;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        float distanceMoved = Vector3.Distance(currentPosition, lastPosition);
        //Debug.Log("distanceMoved: " + distanceMoved);
        if (distanceMoved > movementThresholdAbove)
        {
                CallFunctionOnThresholdReached();
        }
        else if(distanceMoved == movementThresholdBelow)
        {
                CallFunctionOnThresholdDecreased();
        }

        lastPosition = currentPosition;
    }

    private void CallFunctionOnThresholdReached()
    {
        // Call your function when the counter goes above the threshold
        Debug.Log("Threshold reached!");
        // YourFunctionToCallOnThresholdReached();
    }

    private void CallFunctionOnThresholdDecreased()
    {
        // Call your function when the counter goes below the negative threshold
        Debug.Log("Threshold decreased!");
        // YourFunctionToCallOnThresholdDecreased();
    }
}
