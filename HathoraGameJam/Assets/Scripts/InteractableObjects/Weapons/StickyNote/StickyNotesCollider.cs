using HathoraGameJam.CubicleEscape;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HathoraGameJam.CubicleEscape.RollingChairMovement;

public class StickyNotesCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"  )
        {
            other.gameObject.GetComponent<RollingChairMovement>().moveState= MoveState.Stun;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player"  )
        {
            StartCoroutine(ChangeMoveState(other.gameObject));
        }
    }

    float holdDuration=4f;
    IEnumerator ChangeMoveState(GameObject other)
    {
        yield return new WaitForSeconds(holdDuration);
        other.GetComponent<RollingChairMovement>().moveState = MoveState.Normal;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" )
        {
            other.gameObject.GetComponent<RollingChairMovement>().moveState = MoveState.Normal;
        }
    }


}
