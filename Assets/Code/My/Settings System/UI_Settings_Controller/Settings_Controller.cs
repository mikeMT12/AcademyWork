using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
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

    /*    [Space]
        [Header("Rewards Images")]
        [SerializeField] Sprite iconCoinsSprite;*/

    void Start()
    {
        //settingsData.CheckFileExist();
        if (!Directory.Exists(Application.dataPath + "/Resources"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Resources");

        }
        if (!File.Exists(Application.dataPath + "/Resources/XMLSettingsData.xml"))
        {
            File.Create(Application.dataPath + "/Resources/XMLSettingsData.xml");
        }
        else
        {
            print("LoadSettingsInfo");
            settingsData.LoadInfo(true);
        }
        
        
        
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

        /*musicOFFImage.onClick.RemoveAllListeners();
        musicOFFImage.onClick.AddListener(MusicOFFButtonClick);*/

        /*closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(OnCloseButtonClick);*/
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    void OnOpenButtonClick()
    {
        /*offCanvas.SetActive(false);
        settingsCanvas.SetActive(true);*/
        if(!mainMenu)
            GameManager.Instance.UpdateGameState(GameManager.GameState.Settings);
        else
            settingsCanvas.SetActive(true);
            offCanvas.SetActive(false);

        settingsData.LoadInfo(false);
    }

    void OnCloseButtonClick()
    {
        settingsData.SaveInfo();
        Debug.Log(AudioController.volume);
        Debug.Log(settingsData.musicVolume);
        /*offCanvas.SetActive(true);
        settingsCanvas.SetActive(false);*/
        if (!mainMenu)
            GameManager.Instance.UpdateGameState(GameManager.GameState.Pause);
        else
            offCanvas.SetActive(true);
            settingsCanvas.SetActive(false);
            


    }

    void MusicONButtonClick()
    {
        if (AudioController.volume == 0)
        {
            AudioController.Music(musicTurn.GetComponent<Image>(), musicOFFImage, musicONImage, true);
            //AudioController.OnMusic(buttonOn, buttonOff);
        }
        else
        {
            AudioController.Music(musicTurn.GetComponent<Image>(), musicOFFImage, musicONImage, false);
            //AudioController.OffMusic(buttonOn, buttonOff);
        }
 
        //AudioController.OnMusic(musicONImage.gameObject, musicOFFImage.gameObject);
    }

    void MusicOFFButtonClick()
    {
        AudioController.Music(musicTurn.GetComponent<Image>(), musicOFFImage, musicONImage, false);
        //AudioController.OffMusic(musicONImage.gameObject, musicOFFImage.gameObject);
    }

    private void OnApplicationQuit()
    {
        settingsData.SaveInfo();
    }
}
