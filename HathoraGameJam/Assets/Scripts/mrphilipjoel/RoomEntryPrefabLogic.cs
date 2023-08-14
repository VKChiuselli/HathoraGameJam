using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace HathoraGameJam.CubicleEscape
{
    public class RoomEntryPrefabLogic : MonoBehaviour
    {
        public TextMeshProUGUI roomName, playerCount;
        public Button joinButton;

        private void Start()
        {
            transform.localScale = Vector3.one;
        }

    }
}

