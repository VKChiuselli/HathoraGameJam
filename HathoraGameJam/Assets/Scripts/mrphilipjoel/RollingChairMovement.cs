using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HathoraGameJam.CubicleEscape
{
    public class RollingChairMovement : NetworkBehaviour
    {
        private Rigidbody rb;
        public InputActionReference moveAction;
        public float moveForce = 1f;
        public MoveState moveState;

        private Vector3 moveVector = new Vector3(0, 0, 0);
         
        private void Awake()
        {
            moveState = MoveState.Normal;
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }
        }

        public enum MoveState
        {
            Normal,
            DoubleSpeed,
            HalfSpeed,
            Stun
        }


        private void FixedUpdate()
        {
            if (IsClient && IsOwner)
            {

                switch (moveState)
                {

                    case MoveState.Normal:
                        moveVector.x = moveAction.action.ReadValue<Vector2>().x;
                        moveVector.z = moveAction.action.ReadValue<Vector2>().y;
                        rb.AddForce(moveVector * moveForce * Time.deltaTime);
                        break;
                    case MoveState.DoubleSpeed:
                        moveVector.x = moveAction.action.ReadValue<Vector2>().x;
                        moveVector.z = moveAction.action.ReadValue<Vector2>().y;
                        rb.AddForce(moveVector * 2* moveForce * Time.deltaTime);
                        break;
                    case MoveState.HalfSpeed:
                        moveVector.x = moveAction.action.ReadValue<Vector2>().x;
                        moveVector.z = moveAction.action.ReadValue<Vector2>().y;
                        rb.AddForce(moveVector * 0.5f * moveForce * Time.deltaTime);
                        break;
                    case MoveState.Stun:
                        moveVector.x = moveAction.action.ReadValue<Vector2>().x;
                        moveVector.z = moveAction.action.ReadValue<Vector2>().y;
                        rb.AddForce(moveVector * 0f * moveForce * Time.deltaTime);
                        break;
                

                }
           
            }

        }

     public   Animator playerAnimator;
        public void SetPlayerAnimatorCaught(bool animatorBool)
        {
            playerAnimator.SetBool("Caught", animatorBool);
        }
    }
}

