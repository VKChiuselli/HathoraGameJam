using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using HathoraGameJam.CubicleEscape;

public class PlayerSpawnPoints : NetworkBehaviour
{

    public List<Vector3> listSpawnposition;
     GameObject PlayerSpawnPoint;
    GameObject SpawnPointContainer;
    GameObject TargetGroup;
    public override void OnNetworkSpawn()
    {
        if (IsClient && IsOwner)
        {
            /*TargetGroup = GameObject.Find("Target Group");
            CinemachineTargetGroup cinemachineTargetGroup = TargetGroup.GetComponent<CinemachineTargetGroup>();
            CinemachineTargetGroup.Target target;
            target.target = this.transform;
            target.weight = 1;
            target.radius = 1;

            cinemachineTargetGroup.m_Targets.SetValue(target, 0);*/

            TargetGroupManager.instance.AddToGroup(this.transform, 1, 1);


            //    foreach(Transform spawnPoint in SpawnPointContainer.transform)
            //{
            //    listSpawnposition.Add(spawnPoint.position);
            //}

            PlayerSpawnPoint = GameObject.Find("PlayerSpawnPoint");

            if (PlayerSpawnPoint == null)
            {
                Debug.LogError("PlayerSpawnPoint is null");
            }

              listSpawnposition = LoadSpawnPoint();

            transform.position = listSpawnposition[(int)OwnerClientId];
        }
      

    }

    private List<Vector3> LoadSpawnPoint()
    {

        List<Vector3> list = new List<Vector3>();


        foreach (Transform t in PlayerSpawnPoint.transform)
        {
            list.Add(t.position);
        }

      return list;
    }
}
