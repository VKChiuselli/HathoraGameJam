using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawnPoints : NetworkBehaviour
{

    public List<Vector3> listSpawnposition;
    GameObject SpawnPointContainer;
    GameObject TargetGroup;
    public override void OnNetworkSpawn()
    {
        if (IsClient)
        {
            TargetGroup = GameObject.Find("Target Group");
            CinemachineTargetGroup cinemachineTargetGroup = TargetGroup.GetComponent<CinemachineTargetGroup>();
            CinemachineTargetGroup.Target target;
            target.target = this.transform;
            target.weight = 1;
            target.radius = 1;

            cinemachineTargetGroup.m_Targets.SetValue(target, 0);

            //    foreach(Transform spawnPoint in SpawnPointContainer.transform)
            //{
            //    listSpawnposition.Add(spawnPoint.position);
            //}

            transform.position = listSpawnposition[(int)OwnerClientId];
        }
      

    }




}
