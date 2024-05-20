using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public static class AudioController
{
    public static int _volume;

    public static int volume {
        get { return _volume; }
        set { _volume = value; }
    }

    public static void OnMusic(GameObject musicON, GameObject musicOFF)
    {

        AudioListener.volume = 0;
        AudioController.volume = 0;

         musicON.SetActive(false);
         musicOFF.SetActive(true);

        



    }

    public static void OffMusic(GameObject musicON, GameObject musicOFF)
    {
        AudioListener.volume = 1;
        AudioController.volume = 1;

        musicON.SetActive(true);
        musicOFF.SetActive(false);

/*        musicON.gameObject.GetComponent<Button>().enabled = true;
        musicON.enabled = true;
        musicOFF.gameObject.GetComponent<Button>().enabled = false;
        musicOFF.enabled = false;*/
    }

    public static void Music(Image musicButton, Image musicOFF, Image musicON, bool isMusicTurnOn)
    {

        
        if (isMusicTurnOn)
        {
            musicButton.sprite = musicOFF.sprite;
            AudioListener.volume = 1;
            AudioController.volume = 1;

        }
        else
        {
            musicButton.sprite = musicON.sprite;
            AudioListener.volume = 0;
            AudioController.volume = 0;

        }


    }

}
