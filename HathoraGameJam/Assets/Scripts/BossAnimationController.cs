using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Update()
    {
        if (GetComponent<NavMeshAgent>().speed > 0)
        {
            animator.SetBool("Yelling", false);
        }
        else if (GetComponent<NavMeshAgent>().speed == 0)
        {
            animator.SetBool("Yelling", true);
        }

    }
}
