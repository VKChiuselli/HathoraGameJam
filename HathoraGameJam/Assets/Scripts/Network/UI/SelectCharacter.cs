using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode; 
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{

    [SerializeField] List<GameObject> characterList;
    [SerializeField]  GameObject  leftButton;
    [SerializeField]  GameObject  rightButton;

    public int indexCharacter;

    private void Start()
    {

        indexCharacter = 0;

        leftButton.GetComponent<Button>().onClick.AddListener(() => {
            LeftButton();
        });
        rightButton.GetComponent<Button>().onClick.AddListener(() => {
            RightButton();
        });
    }

    public void LeftButton()
    {
        indexCharacter = indexCharacter - 1;
        if (indexCharacter < 0)
        {
            indexCharacter = characterList.Count - 1;
        }

            foreach(GameObject character in characterList)
            {
                character.SetActive(false);
            }
            characterList[indexCharacter].SetActive(true);
          
    }


    public void RightButton()
    {
        indexCharacter = indexCharacter + 1;
        if (indexCharacter > characterList.Count -1)
        {
            indexCharacter = 0;
        }

            foreach(GameObject character in characterList)
            {
                character.SetActive(false);
            }
            characterList[indexCharacter].SetActive(true);
          
    }

}
