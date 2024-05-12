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

    [Header("Buttons")]
    [SerializeField] List<Button> restartGameButtons;
    [SerializeField] List<Button> exitGameButtons;
    //[SerializeField] Button startGameButton;   
    //[SerializeField] Button pauseGameButton;
    //public string pauseKeyCode;

    //[SerializeField] Button loseRestartGameButton;
    [Header("Systems")]
    [SerializeField] private SpawnPoint spawn;
    [SerializeField] private HealthSystem healthSystem;

    [SerializeField] private CutSceneManager cutSceneManager;



    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        playerInput = new PlayerInput();
        playerInput.UISystem.Enable();
        playerInput.UISystem.PauseButton.performed += OnPauseGameButtonClick;
        //playerInput.UISystem.PauseButton.started += OnPauseGameButtonClick;


        //GameManager.OnGameStateChanged.AddListener(UIOnGameStateChanged);
        GameManager.OnGameStateChanged += UIOnGameStateChanged;
        /*startGameButton.onClick.RemoveAllListeners();
        startGameButton.onClick.AddListener(OnStartGameButtonClick);*/

        //pauseGameButton.onClick.RemoveAllListeners();
        //pauseGameButton.onClick.AddListener(OnPauseGameButtonClick);
        

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

    }

    void Start()
    {
        //StartGame();
    }

    private void StartGame()
    {
        
    }

    private void OnExitGameButtonClick()
    {
        Application.Quit();
    }

    private void OnRestartGameButtonClick()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.StartGame);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        /*healthSystem.SetMaxHealth();
        spawn.SpawnPlayer();*/

        
        //SceneManager.LoadScene(1);

    }

    private void OnPauseGameButtonClick(InputAction.CallbackContext context)
    {
        //bool x = context.ReadValue<bool>();
        print($" - pause");
        if (!isPaused)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Pause);
            isPaused = true;
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Game);
            isPaused = false;
        }
            
    }

    /*private void OnStartGameButtonClick()
    {
        //SceneManager.LoadScene(0);
        GameManager.Instance.UpdateGameState(GameManager.GameState.StartGame);
    }*/

    private void UIOnGameStateChanged(GameManager.GameState state)
    {
        /*for(int i = 0; i < windows.Count; i++)
        {
            string name = windows[i].name;
            windows[i].SetActive(state == GameManager.GameState);
        }*/

        print(state);
        
        //startWindow.SetActive(state == GameManager.GameState.StartWindow);
        gameWindow.SetActive(state == GameManager.GameState.Game);
        pauseWindow.SetActive(state == GameManager.GameState.Pause);
        winWindow.SetActive(state == GameManager.GameState.Win);
        loseWindow.SetActive(state == GameManager.GameState.Lose);



    }


    void Update()
    {
        
    }
}
