using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrawlerPreview : MonoBehaviour
{
    [SerializeField] private Button _selectBrawler, _selectSkin;
    [SerializeField] private Image _brawlerImage;
    [SerializeField] private Image _skinImage;
    [SerializeField] private TextMeshProUGUI _name, _description;
    [SerializeField] private TextMeshProUGUI _skinName;
    private Brawler _brawler;
    
    public void ShowPreview(Brawler brawler)
    {
        _brawler = brawler;
        _name.text = brawler.Name;
        _description.text = brawler.Description;
        _brawlerImage.sprite = brawler.Skins[PlayerData.Instance.BrawlersData[brawler.ID].SelectedSkin].Skin;
        bool unlocked = PlayerData.Instance.BrawlersData[brawler.ID].Unlocked;
        _selectBrawler.gameObject.SetActive(unlocked);
        _selectSkin.gameObject.SetActive(unlocked);
        _selectSkin.interactable = PlayerData.Instance.BrawlersData[brawler.ID].SkinUnlocked;
    }

    public void OpenSkinPanel()
    {
        var skinID = PlayerData.Instance.BrawlersData[_brawler.ID].SelectedSkin == 0 ? 1 : 0;
        BrawlerSkin skin = _brawler.Skins[skinID];
        _skinImage.sprite = skin.Skin;
        _skinName.text = skin.SkinName;
    }

    public void SelectSkin()
    {
        var skinID = PlayerData.Instance.BrawlersData[_brawler.ID].SelectedSkin == 0 ? 1 : 0;
        PlayerData.Instance.BrawlersData[_brawler.ID].SelectedSkin = skinID;
        _brawlerImage.sprite = _brawler.Skins[PlayerData.Instance.BrawlersData[_brawler.ID].SelectedSkin].Skin;
        SelectBrawler();
    }

    public void SelectBrawler()
    {
        GameData.Instance.ChangeMainBrawler(_brawler);
    }
}