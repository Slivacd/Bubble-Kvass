using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]  private ShopSkinView _prefab;
    [SerializeField]  private Transform _parent;
    [SerializeField]  private Transform _gemsParent;
    [SerializeField]  private Transform _refreshParent;
    [SerializeField]  private Transform _bgShopParent;
    private List<ShopSkinView> _skins;

    void Start()
    {
        InstantiateSkins();
    }

    private void InstantiateSkins()
    {
        if (_skins != null && _skins.Count > 0)
            foreach (var t in _skins)
                Destroy(t.gameObject);
        _skins = new List<ShopSkinView>();
        var rnd = new System.Random();
        List<Brawler> brawlers = new List<Brawler>(GameData.Instance.GetUnlockedBrawlersWithLockedSkinsList().Where(t => t.Skins.Length > 1).OrderBy(item => rnd.Next()));
        int count = brawlers.Count >= 6 ? 6 : brawlers.Count;
        for (int i = 0; i < count; i++)
        {
            _skins.Add(Instantiate(_prefab,_parent));
            var i1 = i;
            var obj = _skins[i];
            _skins[i].Initialize(brawlers[i].Skins[1], () =>
            {
                if (PlayerData.Instance.Save.Gems >= brawlers[i1].Skins[1].Price)
                {
                    PlayerData.Instance.Save.Gems -= brawlers[i1].Skins[1].Price;
                    PlayerData.Instance.BrawlersData[brawlers[i1].ID].SkinUnlocked = true;
                    Destroy(obj.gameObject);
                }
            });
        }
        _gemsParent.SetAsLastSibling();
        _bgShopParent.SetAsLastSibling();
        _refreshParent.SetAsLastSibling();
    }

    public void RefreshSkins() => Advertisment.Instance.TryRewardedAd(InstantiateSkins);
}