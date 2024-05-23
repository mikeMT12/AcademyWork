using UnityEngine;
using UnityEngine.UI;


public static class AudioController
{
    public static int _volume;

    public static int volume {
        get { return _volume; }
        set { _volume = value; }
    }

    public static void Music(Image musicButton, Image musicOFF, Image musicON, bool isMusicTurnOn)
    {      
        if (isMusicTurnOn)
        {
            musicButton.sprite = musicON.sprite;
            AudioListener.volume = 1;
            AudioController.volume = 1;
        }
        else
        {
            musicButton.sprite = musicOFF.sprite;
            AudioListener.volume = 0;
            AudioController.volume = 0;
        }
    }

}
