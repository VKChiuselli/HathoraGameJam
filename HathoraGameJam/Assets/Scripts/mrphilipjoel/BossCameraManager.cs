using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace HathoraGameJam.CubicleEscape
{
    public class BossCameraManager : MonoBehaviour
    {
        private int targetGroupIndex = -1;
        private Transform localPlayerTransform = null;

        [SerializeField] private float cameraRange = 20f;
        [SerializeField] private float rangeMultiplier = 2f;

        // Update is called once per frame
        void Update()
        {
            if (targetGroupIndex == -1)
            {
                targetGroupIndex = TargetGroupManager.instance.GetTargetIndex(this.transform);
            }

            if (localPlayerTransform == null)
            {
                GetLocalPlayerTransform();
            }
            else if (targetGroupIndex > -1 && localPlayerTransform != null)
            {
                float distance = Vector3.Distance(this.transform.position, localPlayerTransform.position);
                if (distance < cameraRange)
                {
                    float weight = (1 / distance*rangeMultiplier);
                    weight = Mathf.Clamp(weight, .1f, 1);
                    TargetGroupManager.instance.UpdateWeight(targetGroupIndex, weight);
                }
                else
                {
                    TargetGroupManager.instance.UpdateWeight(targetGroupIndex, 0f);
                }
            }
        }

        private void GetLocalPlayerTransform()
        {
            GameObject networkManagerObject = GameObject.Find("NetworkManager");

            if (networkManagerObject != null)
            {
                NetworkManager networkManager = networkManagerObject.GetComponent<NetworkManager>();

                if (networkManager != null && networkManager.LocalClient != null && networkManager.LocalClient.PlayerObject != null)
                {
                    localPlayerTransform = networkManager.LocalClient.PlayerObject.transform;
                }
                else
                {
                    //Debug.LogError("Failed to get the local player transform.");
                }
            }
            else
            {
                //Debug.LogError("NetworkManager object not found.");
            }
        }
    }
}

