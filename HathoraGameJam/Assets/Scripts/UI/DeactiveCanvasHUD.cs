using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DeactiveCanvasHUD : NetworkBehaviour
{
    [SerializeField] GameObject PlayerCanvasHUD;


    void Update()
    {
        if (!IsOwner)
        {
            PlayerCanvasHUD.SetActive(false);
        }
        else
        {
            PlayerCanvasHUD.SetActive(true);
        }
    }
}
