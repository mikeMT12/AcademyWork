using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class Settings_Controller : MonoBehaviour
{
    [Header("Settings UI")]
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject offCanvas;
    [SerializeField] Button openButton;
    [SerializeField] Button closeButton;
    [SerializeField] Button musicTurn;
    [SerializeField] Image musicONImage;    
    [SerializeField] Image musicOFFImage;

    [Space]
    [Header("Rewards Database")]
    [SerializeField] SettingsData settingsData;

    public bool mainMenu;



    void Start()
    {

        if (File.ReadAllText(Application.dataPath + "/Resources/XMLSettingsData.xml", System.Text.Encoding.UTF8) == "")
        {
            AudioController.volume = 1;
            settingsData.SaveInfo();
        }
        print("LoadSettingsInfo");
        settingsData.LoadInfo(true);

               
        Initialize();
        
    }

    private void Initialize()
    {
        //Add click events
        openButton.onClick.RemoveAllListeners();
        openButton.onClick.AddListener(OnOpenButtonClick);

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(OnCloseButtonClick);

        musicTurn.onClick.RemoveAllListeners();
        musicTurn.onClick.AddListener(MusicONButtonClick);

    }


    void OnOpenButtonClick()
    {

        if (!mainMenu)
            GameManager.Instance.UpdateGameState(GameManager.GameState.Settings);
        else
            settingsCanvas.SetActive(true);
            offCanvas.SetActive(false);

        settingsData.LoadInfo(false);
    }

    void OnCloseButtonClick()
    {
        settingsData.SaveInfo();

        if (!mainMenu)
            GameManager.Instance.UpdateGameState(GameManager.GameState.Pause);
        else
            offCanvas.SetActive(true);
            settingsCanvas.SetActive(false);

    }

    void MusicONButtonClick()
    {
        if (AudioController.volume == 0)
            AudioController.Music(musicTurn.GetComponent<Image>(), musicOFFImage, musicONImage, true);

        else
            AudioController.Music(musicTurn.GetComponent<Image>(), musicOFFImage, musicONImage, false);


    }


    private void OnApplicationQuit()
    {
        settingsData.SaveInfo();
    }
}
