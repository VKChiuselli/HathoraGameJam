using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HathoraGameJam.CubicleEscape
{
    public class RollingChairMovement : MonoBehaviour
    {
        private Rigidbody rb;
        public InputActionReference moveAction;
        public float moveForce = 1f;

        private Vector3 moveVector = new Vector3(0, 0, 0);

        private void Awake()
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }
        }

        private void FixedUpdate()
        {
            if (moveAction.action.IsInProgress())
            {
                moveVector.x = moveAction.action.ReadValue<Vector2>().x;
                moveVector.z = moveAction.action.ReadValue<Vector2>().y;
                rb.AddForce(moveVector * moveForce * Time.deltaTime);

            }
        }
    }
}

