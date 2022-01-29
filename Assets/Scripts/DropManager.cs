using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropManager : MonoBehaviour
{
    [SerializeField] private Transform _dropPanel;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private TextMeshProUGUI _coinsCount;
    [SerializeField] private TextMeshProUGUI _gemsCount;
    [SerializeField] private Animation _coinPanel;
    [SerializeField] private Animation _gemPanel;
    [SerializeField] private Animation _brawlerDrop;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Image _brawler;
    [SerializeField] private TextMeshProUGUI _name, _rarity, _desc;
    private DropState _state;
    private Box _box;
    private BoxView _boxView;

    public void AdBox(Box box)
    {
        Advertisment.Instance.TryRewardedAd(() => OpenBox(box));
    }

    public void OpenBox(Box box)
    {
        _box = box;
        _state = DropState.Box;
        if (box.Currency == Currency.Gems && PlayerData.Instance.Save.Gems >= box.Price)
        {
            PlayerData.Instance.Save.Gems -= box.Price;
            Open();
        }
        else if (box.Currency == Currency.Coins && PlayerData.Instance.Save.Coins >= box.Price)
        {
            PlayerData.Instance.Save.Coins -= box.Price;
            Open();
        }

        void Open()
        {
            if (_boxView != null)
                Destroy(_boxView.gameObject);
            Next();
            PlayerData.Instance.Save.BoxesUnlocked++;
            BrawlPass.Instance.UpdateMenuButton();
        }
    }

    public void Next()
    {
        if (_state == DropState.Box)
        {
            _mainPanel.SetActive(false);
            _dropPanel.gameObject.SetActive(true);
            HoldNextButton(0.4f);
            _boxView = Instantiate(_box.Prefab, _dropPanel);
            _boxView.Animator.Play("Enter");
            _state = DropState.Coin;
        }
        else if (_state == DropState.Coin)
        {
            HoldNextButton(0.8f);
            _boxView.OpenEnd += () =>
            {
                OpenCoinsPanel();
                Destroy(_boxView.gameObject);
            };
            _boxView.Animator.Play("Open");
            _state = _box.GemDrop ? DropState.Gem : _box.NewBrawlerDrop ? DropState.Brawler : DropState.End;
        }
        else if (_state == DropState.Gem)
        {
            _coinPanel.gameObject.SetActive(false);
            HoldNextButton(0.4f);
            OpenGemsPanel();
            _state = _box.NewBrawlerDrop ? DropState.Brawler : DropState.End;
        }
        else if (_state == DropState.Brawler)
        {
            _coinPanel.gameObject.SetActive(false);
            _gemPanel.gameObject.SetActive(false);
            Brawler brawler = GameData.Instance.GetLockedBrawler();
            _brawler.sprite = brawler.Skins[0].Skin;
            _name.text = brawler.Skins[0].SkinName;
            _desc.text = brawler.Description;
            _rarity.text = brawler.Rarity;
            PlayerData.Instance.BrawlersData[brawler.ID].Unlocked = true;
            HoldNextButton(0.4f);
            _brawlerDrop.gameObject.SetActive(true);
            _brawlerDrop.Play("Enter");
            _state = DropState.End;
        }
        else if (_state == DropState.End)
        {
            _mainPanel.SetActive(true);
            _brawlerDrop.gameObject.SetActive(false);
            _gemPanel.gameObject.SetActive(false);
            _coinPanel.gameObject.SetActive(false);
            _dropPanel.gameObject.SetActive(false);
        }
    }

    public void OpenCoinsPanel()
    {
        _coinPanel.gameObject.SetActive(true);
        _coinPanel.Play("Enter");
        int coins = _box.GetCoins();
        PlayerData.Instance.Save.Coins += coins;
        _coinsCount.text = $"x{coins}";
    }

    public void OpenGemsPanel()
    {
        _gemPanel.gameObject.SetActive(true);
        _gemPanel.Play("Enter");
        int gems = _box.GetGems();
        PlayerData.Instance.Save.Gems += gems;
        _gemsCount.text = $"x{gems}";
    }

    private void HoldNextButton(float time)
    {
        StartCoroutine(HoldNextButton());

        IEnumerator HoldNextButton()
        {
            _nextButton.interactable = false;
            yield return new WaitForSeconds(time);
            _nextButton.interactable = true;
        }
    }

    enum DropState
    {
        Box,
        Coin,
        Gem,
        Brawler,
        End
    }
}