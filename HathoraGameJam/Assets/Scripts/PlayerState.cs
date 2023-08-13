using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

using UnityEngine.Networking;

public class PlayerState : NetworkBehaviour
{
    private float scoreTick = 0;
    private NetworkVariable<int> score = new NetworkVariable<int>(0);

    private NetworkVariable<bool> caught = new NetworkVariable<bool>(false);

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            score.OnValueChanged += (_, newValue) =>
            {
                ScoreUI.Instance.SetScore(newValue);
            };
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer && !caught.Value)
        {
            scoreTick += Time.deltaTime;
            if (scoreTick > 1)
            {

                score.Value += 10;
                scoreTick = 0;
            }
        }
    }
}
