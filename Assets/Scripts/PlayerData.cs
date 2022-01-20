using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    [HideInInspector] public Data Save;
    public Dictionary<int, BrawlerData> BrawlersData;
    private string _path;

    void Awake()
    {
        Instance = this;
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "GameData")))
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "GameData"));
        _path = Path.Combine(Application.persistentDataPath, "GameData/save.json");
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        if (File.Exists(_path))
            Save = JsonUtility.FromJson<Data>(File.ReadAllText(_path));
        else
        {
            Save = new Data();
            Save.Initialize();
            Debug.Log("New");
        }
        BrawlersData = new Dictionary<int, BrawlerData>();
        for (int i = 0; i < Save.Brawlers.Length; i++)
        {
            BrawlersData.Add(Save.Brawlers[i].ID,Save.Brawlers[i]);
        }
    }

    private void SavePlayerData()
    {
        Save.Brawlers = BrawlersData.Values.ToArray();
        File.WriteAllText(_path, JsonUtility.ToJson(Save));
    }

    private void OnApplicationQuit()
    {
        SavePlayerData();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        SavePlayerData();
    }

    [MenuItem("Tools/Open Save Path")]
    public static void OpenSavePath()
    {
        Debug.Log(Path.Combine(Application.persistentDataPath, "GameData"));
        Application.OpenURL(Path.Combine(Application.persistentDataPath, "GameData"));
    }

    [MenuItem("Tools/Delete Save")]
    public static void DeleteSave()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "GameData/save.json")))
            File.Delete(Path.Combine(Application.persistentDataPath, "GameData/save.json"));
    }
}

[System.Serializable]
public class Data
{
    public int AvatarID;
    public int BackgroundID;
    public int BoxesUnlocked;
    public string PlayersName;
    public int Gems, Coins;
    public BrawlerData[] Brawlers;
    public bool[] BrawlPassRewardClaimed;
    public bool[] Backgrounds;

    public void Initialize()
    {
        Brawlers = new BrawlerData[GameData.Instance.Brawlers.Values.Count()];
        BrawlPassRewardClaimed = new bool[GameData.Instance.BrawlPassRewards.Count];
        Backgrounds = new bool[GameData.Instance.Backgrounds.Length];
        Backgrounds[0] = true;
        for (int i = 0; i < GameData.Instance.Brawlers.Values.Count(); i++)
            Brawlers[i] = new BrawlerData(GameData.Instance.Brawlers[i].ID);
    }
}

[System.Serializable]
public class BrawlerData
{
    public int ID;
    public bool Unlocked;
    public int SelectedSkin;
    public bool SkinUnlocked;
    
    public BrawlerData(int id)
    {
        ID = id;
    }
}