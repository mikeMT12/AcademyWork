using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    

    [Header("Buttons")]
    [SerializeField] Button startGameButton;
    [SerializeField] Button exitGameButton;
    // Start is called before the first frame update


    private void Awake()
    {
       

        startGameButton.onClick.RemoveAllListeners();
        startGameButton.onClick.AddListener(OnStartGameButtonClick);

        exitGameButton.onClick.RemoveAllListeners();
        exitGameButton.onClick.AddListener(OnExitGameButtonClick);

    }

    private void OnExitGameButtonClick()
    {
        Application.Quit();
        
    }

    private void OnStartGameButtonClick()
    {
        SceneManager.LoadScene(1);
    }

}
 