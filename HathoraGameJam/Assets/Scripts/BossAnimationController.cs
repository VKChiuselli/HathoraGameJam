using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
 
  
     
 
    public void SetAnimator(bool animated)
    {
        animator.SetBool("Yelling", animated);
    }
}
