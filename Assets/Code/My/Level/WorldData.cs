using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;
using System.IO;

[System.Serializable]
public class WorldData 
{
/*    public int coins = 0;
    public bool ferst_time;*/

    public int crowns = 0;

    /*public static int Coins
    {
        get{ return _coins; }
        set {
            _coins = value;
            
        }
    }*/


    public static void SaveInfo(int Crowns)
    {
        string filePath = Application.dataPath + "/Resources/XMLWorldData.xml";


        XmlSerializer formatter = new XmlSerializer(typeof(WorldData));

        //List<int> rewards = new List<int>();
        var data = new WorldData()
        {
            //coins = Coins,
            // = ferst_time
            crowns = Crowns
        };

        
        //DailyReward_Data.nowReward;

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
            //ferst_time = worldData.ferst_time;

        }
    }

    public void CheckFerstTime()
    {

    }

    /*public int GetCoins()
    {
        return coins;
    }*/
}
