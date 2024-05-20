using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    [Header("Systems")]
    [SerializeField] private PlayerAnimatorController playerAnimatorController;
    [SerializeField] private PhysicsMovement movementController;
    [SerializeField] private SoundAndMusicSystem soundSystem;
    [SerializeField] private CutSceneManager cutSceneManager;
    [SerializeField] private TherdCamera therdCamera;
    [SerializeField] private WorldInfo worldInfo;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Timer timer;
 



    public enum GameState
    {
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
    }

    void Start()
    {
        UpdateGameState(GameState.StartGame);
    }

    public void UpdateGameState(GameState newState)
    {
        lastState = state;
        state = newState;

        switch (newState)
        {
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
        timer.timeFlowText.gameObject.SetActive(false);
    }

    private void HandleLoseGame()
    {
        movementController.enabled = false;
        therdCamera.enabled = false;
        timer.StopTimer();
        Cursor.lockState = CursorLockMode.Confined;
        playerAnimatorController.SetDeathAnimation();
        soundSystem.loseSound.Play();
    }

    private void HandleWinGame()
    {
        therdCamera.enabled = false;
        movementController.enabled = false;
        timer.StopTimer();
        Cursor.lockState = CursorLockMode.Confined;
        playerAnimatorController.SetFinishAnimation();
        worldInfo.PlusCrowns();
        soundSystem.winSound.Play();
    }

    private void HandlePauseGame()
    {
        //soundSystem.inGameMusic.Stop();
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
        if(lastState != GameState.Pause)
        {
            soundSystem.inGameMusic.Play();
        }        
        therdCamera.gameObject.SetActive(true);
        movementController.enabled = true;
        timer.timeFlowText.color = Color.black;
        timer.ContinueTimer();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void HandleStartGame()
    {
        cutSceneManager.PlayCutScene();
        soundSystem.startGameMusic.Play();
        movementController.enabled = false;
        therdCamera.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        timer.timeFlowText.gameObject.SetActive(false);
        if (FerstTime)
        {
            Debug.Log(cutSceneManager.over);
            Debug.Log(GameState.StartGame);
            FerstTime = false;
        }
        else if (cutSceneManager.over)
        {
            StartCountdown();        
        }
    }

    public void GameStateFromCutScene()
    {
        UpdateGameState(GameState.Game);
    }

    public void StartCountdown()
    {
        Debug.Log("StartCountdown");
        soundSystem.startGameMusic.Stop();
        therdCamera.gameObject.SetActive(true);
        uiManager.StartCountdown();
    }
}