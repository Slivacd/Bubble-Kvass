using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrawlerPreviewButtonView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _rarity;
    [SerializeField] private TextMeshProUGUI _name;
    public Button Button;

    public void Initialize(Brawler brawler)
    {
        _icon.sprite = brawler.Skins[0].Skin;
        _icon.color = PlayerData.Instance.BrawlersData[brawler.ID].Unlocked ? Color.white : Color.black;
        _name.text = PlayerData.Instance.BrawlersData[brawler.ID].Unlocked ? brawler.Skins[0].SkinName : "ЗАКРЫТО";
        _rarity.color = brawler.RarityColor;
    }
}
