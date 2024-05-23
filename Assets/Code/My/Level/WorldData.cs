using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[System.Serializable]
public class WorldData 
{
    public int crowns = 0;

    public static void SaveInfo(int Crowns)
    {
        string filePath = Application.dataPath + "/Resources/XMLWorldData.xml";
        XmlSerializer formatter = new XmlSerializer(typeof(WorldData));
        var data = new WorldData()
        {
            crowns = Crowns
        };

        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
        }

        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, data);
        }
    }

    public void LoadInfo()
    {

        XmlSerializer formatter = new XmlSerializer(typeof(WorldData));
        string filePath = Application.dataPath + "/Resources/XMLWorldData.xml";
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            WorldData worldData = (WorldData)formatter.Deserialize(fs);
            crowns = worldData.crowns;

        }
    }

}
