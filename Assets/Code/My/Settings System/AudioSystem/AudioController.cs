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

    public static void OnMusic(Image musicON, Image musicOFF)
    {

        AudioListener.volume = 0;
        AudioController.volume = 0;

         musicON.gameObject.SetActive(false);
         musicOFF.gameObject.SetActive(true);

        /*musicON.gameObject.GetComponent<Button>().enabled = false;
        musicON.enabled = false;
        musicOFF.gameObject.GetComponent<Button>().enabled = true;
        musicOFF.enabled = true;*/

    }

    public static void OffMusic(Image musicON, Image musicOFF)
    {
        AudioListener.volume = 1;
        AudioController.volume = 1;

        musicON.gameObject.SetActive(true);
        musicOFF.gameObject.SetActive(false);

/*        musicON.gameObject.GetComponent<Button>().enabled = true;
        musicON.enabled = true;
        musicOFF.gameObject.GetComponent<Button>().enabled = false;
        musicOFF.enabled = false;*/
    }
    
}
