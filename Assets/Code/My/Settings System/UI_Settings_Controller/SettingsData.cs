using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using System.Xml.Serialization;

[System.Serializable]
public class SettingsData 
{
    public int musicVolume;
    public string buttonOnName, buttonOffName;
    private string filePath;


    public void CheckFileExist()
    {
       
        
        //SaveInfo();
        Debug.Log(filePath);
       
        //musicVolume = AudioController.volume;
       
    }

    public void SaveInfo()
    {
        XmlSerializer formatter = new XmlSerializer(typeof(SettingsData));
        filePath = Application.dataPath + "/Resources/XMLSettingsData.xml";
        var settings = new SettingsData
        {
            musicVolume = AudioController.volume
           
        };

        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, settings);
        }

    }

    public void LoadInfo(bool start)
    {
       
        
        XmlSerializer formatter = new XmlSerializer(typeof(SettingsData));  
        filePath = Application.dataPath + "/Resources/XMLSettingsData.xml";
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            SettingsData settingsData = (SettingsData)formatter.Deserialize(fs);
            AudioController.volume = settingsData.musicVolume;
            Debug.Log(settingsData.musicVolume);
        }
        if (start)
        {
            AudioListener.volume = AudioController.volume;
            Debug.Log(AudioController.volume);
        }
        else
        {
            AudioSet();
        }
         
    }

    public void AudioSet()
    {
        GameObject buttonOn = GameObject.FindWithTag("MusicOnButton");
        GameObject buttonOff = GameObject.FindWithTag("MusicOffButton");
        /*buttonOff.GetComponent<Image>().enabled = false;
        buttonOff.GetComponent<Button>().enabled = false;*/


        if (AudioController.volume == 0)
        {
            AudioController.OnMusic(buttonOn.GetComponent<Image>(), buttonOff.GetComponent<Image>());
        }
        else
        {
            AudioController.OffMusic(buttonOn.GetComponent<Image>(), buttonOff.GetComponent<Image>());
        }
    }
}
