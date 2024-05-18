using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Playables;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state;
    public GameState lastState;
    public static bool FerstTime = true;

    public static event Action<GameState> OnGameStateChanged;
    //public static UnityEvent<GameState> OnGameStateChanged;

    [SerializeField] private PlayerAnimatorController playerAnimatorController;
    [SerializeField] private PhysicsMovement movementController;
    [SerializeField] private CutSceneManager cutSceneManager;
    [SerializeField] private TherdCamera therdCamera;
    [SerializeField] private WorldInfo worldInfo;
    [SerializeField] private Timer timer;



    public enum GameState
    {
        //StartWindow,
        StartGame,
        Game,
        Pause,
        Settings,
        Win,
        Lose
    }


    private void Awake()
    {
        Instance = this;
        //DontDestroyOnLoad(this.gameObject);
        //UpdateGameState(GameState.StartWindow);
    }

    void Start()
    {
       
        if (FerstTime)
            UpdateGameState(GameState.StartGame);
        
        else
            UpdateGameState(GameState.Game);

    }

    public void UpdateGameState(GameState newState)
    {
        lastState = state;
        state = newState;

        switch (newState)
        {
            /*case GameState.StartWindow:
                HandleStartWindow();
                break;*/
            case GameState.StartGame:
                HandleStartGame();
                break;
            case GameState.Game:
                HandleGame();
                break;
            case GameState.Pause:
                HandlePauseGame();
                break;
            case GameState.Settings:
                HandleSettingsGame();
                break;
            case GameState.Win:
                HandleWinGame();
                break;
            case GameState.Lose:
                HandleLoseGame();
                break;

        }

        OnGameStateChanged?.Invoke(newState);

    }

    private void HandleSettingsGame()
    {
        
    }

    private void HandleLoseGame()
    {
        //Time.timeScale = 0;
        movementController.enabled = false;
        therdCamera.enabled = false;
        timer.StopTimer();
        Cursor.lockState = CursorLockMode.Confined;
        playerAnimatorController.SetDeathAnimation();

    }

    private void HandleWinGame()
    {
        //ime.timeScale = 0;
        therdCamera.enabled = false;
        movementController.enabled = false;
        timer.StopTimer();
        Cursor.lockState = CursorLockMode.Confined;
        playerAnimatorController.SetFinishAnimation();
        worldInfo.PlusCrowns();

    }

    private void HandlePauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        if (!FerstTime)
        {
            timer.StopTimer();
        }
        
        
    }

    private void HandleGame()
    { 
        Time.timeScale = 1;
        therdCamera.gameObject.SetActive(true);
        movementController.enabled = true;
        timer.timeFlowText.color = Color.black;
        timer.ContinueTimer();
        Cursor.lockState = CursorLockMode.Locked;
    }

    /*private void HandleStartWindow()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }*/

    private void HandleStartGame()
    {
        Time.timeScale = 1;
        timer.timeFlowText.gameObject.SetActive(false);
        cutSceneManager.PlayCutScene();
        Debug.Log(cutSceneManager.over);
        Debug.Log(GameState.StartGame);
        movementController.enabled = false;
        therdCamera.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        FerstTime = false;
    }

    public void GameStateFromCutScene()
    {
        UpdateGameState(GameState.Game);
    }
}