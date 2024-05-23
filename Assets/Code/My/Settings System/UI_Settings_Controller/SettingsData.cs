using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Xml.Serialization;

[System.Serializable]
public class SettingsData 
{
    public int musicVolume=1;
    public string MusicOnButtonImage, MusicOffButtonImage;
    private string filePath;


    public void CheckFileExist()
    {
        Debug.Log(filePath);  
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
        }
        if (start)
        {
            AudioListener.volume = AudioController.volume;
        }
        else
        {
            AudioSet();
        }         
    }

    public void AudioSet()
    {
        GameObject musicONImage = GameObject.Find("MusicOnButtonImage");
        GameObject musicOFFImage = GameObject.Find("MusicOffButtonImage");
        GameObject musicTurn = GameObject.Find("MusicTurnButton");

        if (AudioController.volume == 0)
        {
            AudioController.Music(musicTurn.GetComponent<Image>(), musicOFFImage.GetComponent<Image>(), musicONImage.GetComponent<Image>(), false);
        }
        else
        {
            AudioController.Music(musicTurn.GetComponent<Image>(), musicOFFImage.GetComponent<Image>(), musicONImage.GetComponent<Image>(), true);
        }
    }
}
