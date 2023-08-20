using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace HathoraGameJam.CubicleEscape
{
    public class TitleMenu : MonoBehaviour
    {
        [Header("Credits Panels")]
        public GameObject leftPanel;
        public GameObject rightPanel;
        public GameObject TitleScenePrefab;
        public GameObject MainMenuScenePrefab;
        public GameObject MainCameraTitleScene;
        public GameObject MainCameraGame;

        private void Start()
        {
       
        }

        public void Credits()
        {
            leftPanel.active = (!leftPanel.active);
            rightPanel.active = (!rightPanel.active);
        }

        public void PrevScene()
        {
            TitleScenePrefab.SetActive(true);
            MainMenuScenePrefab.SetActive(true);
            MainCameraGame.SetActive(false);
            MainCameraTitleScene.SetActive(true);
            //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        
    }
}

