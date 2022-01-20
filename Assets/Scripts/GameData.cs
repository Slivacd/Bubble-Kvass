using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{

    public static GameData Instance;
    public IReadOnlyDictionary<int,Brawler> Brawlers { get; private set; }
    public List<BrawlPassReward> BrawlPassRewards;
    public Background[] Backgrounds;
    public float Border => _mainCamera.orthographicSize * _mainCamera.aspect;
    public bool NetworkAvailable => Application.internetReachability != NetworkReachability.NotReachable;
    [SerializeField] private Image _mainBrawlerImage;
    private Camera _mainCamera;
    
    void Awake()
    {
        Instance = this;
        _mainCamera = Camera.main;
        var brawlers = Resources.LoadAll<Brawler>("Brawlers");
        Brawlers = brawlers.ToDictionary(t => t.ID);
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
}
