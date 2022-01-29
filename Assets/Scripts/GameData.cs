using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameData : MonoBehaviour
{

    public static GameData Instance;
    public IReadOnlyDictionary<int,Brawler> Brawlers { get; private set; }
    public List<BrawlPassReward> BrawlPassRewards;
    public Background[] Backgrounds;
    public float Border => _mainCamera.orthographicSize * _mainCamera.aspect;
    public bool NetworkAvailable => Application.internetReachability != NetworkReachability.NotReachable;
    [SerializeField] private Image _mainBrawlerImage;
    [SerializeField] private TextMeshProUGUI[] _coins, _gems;
    private Camera _mainCamera;
    
    void Awake()
    {
        Instance = this;
        _mainCamera = Camera.main;
        var brawlers = Resources.LoadAll<Brawler>("Brawlers");
        Brawlers = brawlers.ToDictionary(t => t.ID);
    }

    private void Start()
    {
        ChangeMainBrawler(Brawlers[PlayerData.Instance.Save.MainBrawlerID]);
    }

    private void Update()
    {
        foreach (var t in _coins)
            t.text = PlayerData.Instance.Save.Coins.ToString();
        foreach (var t in _gems)
            t.text = PlayerData.Instance.Save.Gems.ToString();
    }

    public void ChangeMainBrawler(Brawler brawler)
    {
        _mainBrawlerImage.sprite = brawler.Skins[PlayerData.Instance.BrawlersData[brawler.ID].SelectedSkin].Skin;
    }

    public Brawler GetLockedBrawler()
    {
        List<BrawlerData> brawlers = PlayerData.Instance.BrawlersData.Values.Where(t => !t.Unlocked).ToList();
        return Brawlers[brawlers[Random.Range(0,brawlers.Count)].ID];
    }

    public IReadOnlyList<Brawler> GetUnlockedBrawlersList()
    {
        return (IReadOnlyList<Brawler>) Brawlers.Values.Where(t => PlayerData.Instance.BrawlersData[t.ID].Unlocked);
    }
    
    public IEnumerable<Brawler> GetUnlockedBrawlersWithLockedSkinsList()
    {
        return Brawlers.Values.Where(t => PlayerData.Instance.BrawlersData[t.ID].Unlocked && !PlayerData.Instance.BrawlersData[t.ID].SkinUnlocked);
    }
}
