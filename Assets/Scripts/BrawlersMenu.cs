using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrawlersMenu : MonoBehaviour
{
    [SerializeField] private BrawlerPreviewButtonView _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private BrawlerPreview _brawlerPreview;
    [SerializeField] private GameObject _brawlerMenuObject, _brawlerPreviewObject;
    private List<BrawlerPreviewButtonView> _brawlersButton;


    void Start()
    {
        _brawlersButton = new List<BrawlerPreviewButtonView>();
        for (int i = 0; i < GameData.Instance.Brawlers.Values.Count(); i++)
        {
            _brawlersButton.Add(Instantiate(_prefab, _parent));
            var i1 = i;
            _brawlersButton[i].Button.onClick.AddListener(() =>
            {
                _brawlerPreview.ShowPreview(GameData.Instance.Brawlers[i1]);
                _brawlerMenuObject.SetActive(false);
                _brawlerPreviewObject.SetActive(true);
            });
        }
    }

    public void OpenBrawlersMenu()
    {
        for (int i = 0; i < _brawlersButton.Count; i++)
        {
            _brawlersButton[i].Image.sprite = PlayerData.Instance.BrawlersData[i].Unlocked
                ? GameData.Instance.Brawlers[i].UnlockedIcon
                : GameData.Instance.Brawlers[i].LockedIcon;
        }

        _count.text =
            $"{PlayerData.Instance.BrawlersData.Values.Count(t => t.Unlocked)} из {GameData.Instance.Brawlers.Values.Count()} ТИПОВ";
    }
}