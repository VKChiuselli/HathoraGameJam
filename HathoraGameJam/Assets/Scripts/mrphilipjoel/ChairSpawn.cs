using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HathoraGameJam.CubicleEscape
{
    public class ChairSpawn : MonoBehaviour
    {
        public GameObject chairPrefab;

        private void Awake()
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, 0f, transform.position.z);
            GameObject chair = GameObject.Instantiate(chairPrefab, transform.position, transform.rotation);
            chair.GetComponent<ChairFollow>().sphereTransform = transform;
            GetComponent<RollingChairMovement>().playerAnimator = chair.transform.GetChild(0).gameObject.GetComponent<Animator>();
        }

      
    }
}

