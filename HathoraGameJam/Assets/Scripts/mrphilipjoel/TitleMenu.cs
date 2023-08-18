using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace HathoraGameJam.CubicleEscape
{
    public class TitleMenu : MonoBehaviour
    {
        public void Play()
        {
   //         SceneManager.LoadScene(1);
        }

        public void Credits()
        {
            Debug.LogWarning("Credits Screen has not been created yet!");
        }

        public void PrevScene()
        {
  //          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        
    }
}

