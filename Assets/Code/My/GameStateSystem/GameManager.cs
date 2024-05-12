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

    public static event Action<GameState> OnGameStateChanged;
    //public static UnityEvent<GameState> OnGameStateChanged;

    [SerializeField] private PlayerAnimatorController playerAnimatorController;
    [SerializeField] private PhysicsMovement movementController;
    [SerializeField] private CutSceneManager cutSceneManager;
    [SerializeField] private TherdCamera therdCamera;



    public enum GameState
    {
        //StartWindow,
        StartGame,
        Game,
        Pause,
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
        UpdateGameState(GameState.StartGame);
    }

    public void UpdateGameState(GameState newState)
    {
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
            case GameState.Win:
                HandleWinGame();
                break;
            case GameState.Lose:
                HandleLoseGame();
                break;

        }

        OnGameStateChanged?.Invoke(newState);

    }

    private void HandleLoseGame()
    {
        //Time.timeScale = 0;
        movementController.enabled = false;
        therdCamera.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        playerAnimatorController.SetDeathAnimation();

    }

    private void HandleWinGame()
    {
        //ime.timeScale = 0;
        therdCamera.enabled = false;
        movementController.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        playerAnimatorController.SetFinishAnimation();
    }

    private void HandlePauseGame()
    {
        Debug.Log("pause");
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        
    }

    private void HandleGame()
    {
        Debug.Log("game");
        
        Time.timeScale = 1;
        movementController.enabled = true;
        therdCamera.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    /*private void HandleStartWindow()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }*/

    private void HandleStartGame()
    {
        Debug.Log("Startgame");
        Time.timeScale = 1;
        print(cutSceneManager.num);
        cutSceneManager.PlayCutScene();
        movementController.enabled = false;
        therdCamera.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameStateFromCutScene()
    {
        print("SSSS");
        UpdateGameState(GameState.Game);
        //OnGameStateChanged?.Invoke(GameState.Game);
    }
}