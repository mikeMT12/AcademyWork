using UnityEngine;
using TMPro;
using System.IO;

public class WorldInfo : MonoBehaviour
{
    [Header("Crowns UI")]
    [SerializeField] TextMeshProUGUI crownsText;

    [Header("Rewards Database")]
    [SerializeField] WorldData worldData;

    private void Awake()
    {
        if (!Directory.Exists(Application.dataPath + "/Resources"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Resources");
        }
        if (!File.Exists(Application.dataPath + "/Resources/XMLWorldData.xml"))
        {
            File.Create(Application.dataPath + "/Resources/XMLWorldData.xml");

        }

        if(!File.Exists(Application.dataPath + "/Resources/XMLSettingsData.xml"))
        {
            print("CreateSettingsInfo");
            File.Create(Application.dataPath + "/Resources/XMLSettingsData.xml");

        }
    }

    void Start()
    {
        print(File.ReadAllText(Application.dataPath + "/Resources/XMLWorldData.xml", System.Text.Encoding.UTF8));
        if (File.ReadAllText(Application.dataPath + "/Resources/XMLWorldData.xml", System.Text.Encoding.UTF8) == "")
        {
            WorldData.SaveInfo(0);
        }

        worldData.LoadInfo();

        Initialize();
    }

    private void Initialize()
    {
        UpdateCrownsTextUI();
    }

    private void UpdateCrownsTextUI()
    {
        crownsText.text = worldData.crowns.ToString();
    }


    public void PlusCrowns()
    {
        Debug.Log($"короны - {worldData.crowns}");
        worldData.crowns += 1;
        WorldData.SaveInfo(worldData.crowns);
        UpdateCrownsTextUI();
    }

    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        WorldData.SaveInfo(worldData.crowns);
    }
}
