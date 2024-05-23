using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public PlayerInput playerInput;
    bool isPaused = false;


    [Header("Windows to show")]
    [SerializeField] private GameObject gameWindow;
    [SerializeField] private GameObject pauseWindow;
    [SerializeField] private GameObject winWindow;
    [SerializeField] private GameObject loseWindow;
    [SerializeField] private GameObject settingsWindow;

    public List<GameObject> countdownNums;

    [Header("Buttons")]
    [SerializeField] List<Button> restartGameButtons;
    [SerializeField] List<Button> exitGameButtons;
    [SerializeField] Button pauseExitGameButton;

    [Header("Systems")]
    [SerializeField] private SpawnPoint spawn;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private SoundAndMusicSystem soundSystem;
    [SerializeField] private CutSceneManager cutSceneManager;



    private void Start()
    {
        playerInput = PhysicsMovement.playerInput;
        playerInput.UISystem.Enable();
    }

    public void Init()
    {
        playerInput.UISystem.PauseButton.performed += OnPauseGameButtonClick;

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
        
        playerInput.UISystem.PauseButton.performed -= OnPauseGameButtonClick;
        GameManager.OnGameStateChanged -= UIOnGameStateChanged;
        
    }

    private void OnExitGameButtonClick()
    {
        Application.Quit();
    }

    private void OnRestartGameButtonClick()
    {
        cutSceneManager.Skip();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void OnPauseGameButtonClick(InputAction.CallbackContext context)
    {
        
        if (isPaused && GameManager.Instance.state != GameManager.GameState.Settings)
        {
            isPaused = false;
            GameManager.Instance.UpdateGameState(GameManager.GameState.Game);
            Time.timeScale = 1;
        }
        else if(!isPaused && GameManager.Instance.state == GameManager.GameState.Game)
        {
            isPaused = true;
            GameManager.Instance.UpdateGameState(GameManager.GameState.Pause);  
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


    public IEnumerator Countdown()
    {
        int time = 0;
        while (time != countdownNums.Count)
        {
            countdownNums[time].SetActive(true);
            if(time != countdownNums.Count-1)
                soundSystem.countdownNums.Play();
            else
                soundSystem.countdownGo.Play();
            yield return new WaitForSeconds(1);
            countdownNums[time].SetActive(false);
            time++;
        }
        Init();
        GameManager.Instance.UpdateGameState(GameManager.GameState.Game);
       
    }

    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }
}
