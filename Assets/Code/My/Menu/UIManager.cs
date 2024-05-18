using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private PlayerInput playerInput;
    bool isPaused = false;

    [Header("Windows to show")]
    //[SerializeField] private List<GameObject> windows;
    //[SerializeField] private GameObject startWindow;
    [SerializeField] private GameObject gameWindow;
    [SerializeField] private GameObject pauseWindow;
    [SerializeField] private GameObject winWindow;
    [SerializeField] private GameObject loseWindow;
    [SerializeField] private GameObject settingsWindow;

    [Header("Buttons")]
    [SerializeField] List<Button> restartGameButtons;
    [SerializeField] List<Button> exitGameButtons;
    //[SerializeField] Button startGameButton;   
    [SerializeField] Button pauseExitGameButton;
    //public string pauseKeyCode;

    //[SerializeField] Button loseRestartGameButton;
    [Header("Systems")]
    [SerializeField] private SpawnPoint spawn;
    [SerializeField] private HealthSystem healthSystem;

    [SerializeField] private CutSceneManager cutSceneManager;



    [SerializeField] private GameObject DDDD;


    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        playerInput = new PlayerInput();
        playerInput.UISystem.Enable();
        playerInput.UISystem.PauseButton.performed += OnPauseGameButtonClick;
        //playerInput.UISystem.PauseButton.started += OnPauseGameButtonClick;

        GameManager.OnGameStateChanged += UIOnGameStateChanged;
        
        for(int i = 0; i < restartGameButtons.Count; i++)
        {
            restartGameButtons[i].onClick.RemoveAllListeners();
            restartGameButtons[i].onClick.AddListener(OnRestartGameButtonClick);
        }

        for (int i = 0; i < exitGameButtons.Count; i++)
        {
            exitGameButtons[i].onClick.RemoveAllListeners();
            exitGameButtons[i].onClick.AddListener(OnExitGameButtonClick);
        }

        pauseExitGameButton.onClick.RemoveAllListeners();
        pauseExitGameButton.onClick.AddListener(OnPauseExitGameButtonClick);
    }


    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= UIOnGameStateChanged;
        playerInput.UISystem.PauseButton.performed -= OnPauseGameButtonClick;
    }

    private void OnExitGameButtonClick()
    {
        Application.Quit();
    }

    private void OnRestartGameButtonClick()
    {
        // GameManager.Instance.UpdateGameState(GameManager.GameState.StartGame);

        //SceneManager.LoadScene(0);
        //cutSceneManager.enabled = false;
        cutSceneManager.Skip();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        /*healthSystem.SetMaxHealth();
        spawn.SpawnPlayer();*/

        
        //SceneManager.LoadScene(1);

    }

    private void OnPauseGameButtonClick(InputAction.CallbackContext context)
    {
        //bool x = context.ReadValue<bool>();
        if (!isPaused)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Pause);
            isPaused = true;
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameManager.Instance.lastState);
            isPaused = false;
            Time.timeScale = 1;
        }
            
    }

    private void OnPauseExitGameButtonClick()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Game);
        isPaused = false;
    }


    private void UIOnGameStateChanged(GameManager.GameState state)
    {
        pauseWindow.SetActive(state == GameManager.GameState.Pause);
        winWindow.SetActive(state == GameManager.GameState.Win);
        loseWindow.SetActive(state == GameManager.GameState.Lose);
        settingsWindow.SetActive(state == GameManager.GameState.Settings);
        gameWindow.SetActive(state == GameManager.GameState.Game);
    }
}
