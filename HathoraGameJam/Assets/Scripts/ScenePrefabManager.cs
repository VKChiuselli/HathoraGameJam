using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenePrefabManager : MonoBehaviour
{

    [SerializeField] GameObject TitleScenePrefab;
    [SerializeField] Button TitleScenePrefabPlayButton;
    [SerializeField] GameObject MainMenuScenePrefab;


    void Start()
    {
        TitleScenePrefabPlayButton.onClick.AddListener(GoToMainMenuScenePrefab);
        TitleScenePrefab.SetActive(true);
        MainMenuScenePrefab.SetActive(false);
    }

    private void GoToMainMenuScenePrefab()
    {
        MainMenuScenePrefab.SetActive(true);
        TitleScenePrefab.SetActive(false);
    }

    public void StartGame()
    {
        MainMenuScenePrefab.SetActive(false);
        TitleScenePrefab.SetActive(false);
    }
}
