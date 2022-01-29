using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrawlPass : MonoBehaviour
{
    [SerializeField] private Slider _brawlPassMenuSlider;
    [SerializeField] private Slider _mainMenuSlider;
    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _levelTextParent;
    [SerializeField] private BrawlPassRewardView _prefab;
    [SerializeField] private Sprite _gemIcon, _coinIcon;
    [SerializeField] private TextMeshProUGUI _brawlPassLevelText;
    [SerializeField] private TextMeshProUGUI _brawlPassProgressText;
    [SerializeField] private TextMeshProUGUI _levelTextPrefab;
    private const int BOXES_TO_UNLOCK_LEVEL = 15;
    private List<BrawlPassRewardView> _brawlPassRewardViews;

    private int BrawlPassLevel
    {
        get
        {
            if (Mathf.FloorToInt(PlayerData.Instance.Save.BoxesUnlocked / BOXES_TO_UNLOCK_LEVEL) <=
                GameData.Instance.BrawlPassRewards.Count)
                return Mathf.FloorToInt(PlayerData.Instance.Save.BoxesUnlocked / BOXES_TO_UNLOCK_LEVEL);
            return GameData.Instance.BrawlPassRewards.Count;
        }
    }

    public static BrawlPass Instance;

    void Start()
    {
        Instance = this;
        _brawlPassRewardViews = new List<BrawlPassRewardView>();
        _mainMenuSlider.maxValue = BOXES_TO_UNLOCK_LEVEL;
        _brawlPassMenuSlider.maxValue = GameData.Instance.BrawlPassRewards.Count * BOXES_TO_UNLOCK_LEVEL;
        _brawlPassMenuSlider.value = PlayerData.Instance.Save.BoxesUnlocked;
        for (int i = 0; i < GameData.Instance.BrawlPassRewards.Count; i++)
        {
            _brawlPassRewardViews.Add(Instantiate(_prefab, _parent));
            Instantiate(_levelTextPrefab, _levelTextParent).text = (i + 1).ToString();
            if (GameData.Instance.BrawlPassRewards[i].RewardType == RewardType.Coins)
            {
                _brawlPassRewardViews[i].Image.sprite = _coinIcon;
                _brawlPassRewardViews[i].Text.text = GameData.Instance.BrawlPassRewards[i].Value.ToString();
            }
            else if (GameData.Instance.BrawlPassRewards[i].RewardType == RewardType.Gems)
            {
                _brawlPassRewardViews[i].Image.sprite = _gemIcon;

                _brawlPassRewardViews[i].Text.text = GameData.Instance.BrawlPassRewards[i].Value.ToString();
            }
            else if (GameData.Instance.BrawlPassRewards[i].RewardType == RewardType.Brawler &&
                     !GameData.Instance.BrawlPassRewards[i].Skin)
            {
                _brawlPassRewardViews[i].Image.sprite = GameData.Instance.Brawlers[GameData.Instance.BrawlPassRewards[i].Value].Skins[0].Skin;
                _brawlPassRewardViews[i].Text.text = GameData.Instance
                    .Brawlers[GameData.Instance.BrawlPassRewards[i].Value].Skins[0].SkinName;
            }
            else if (GameData.Instance.BrawlPassRewards[i].RewardType == RewardType.Brawler &&
                     GameData.Instance.BrawlPassRewards[i].Skin)
            {
                _brawlPassRewardViews[i].Image.sprite = GameData.Instance.Brawlers[GameData.Instance.BrawlPassRewards[i].Value].Skins[1].Skin;
                _brawlPassRewardViews[i].Text.text = GameData.Instance
                    .Brawlers[GameData.Instance.BrawlPassRewards[i].Value].Skins[1].SkinName;
            }

            var i1 = i;
            _brawlPassRewardViews[i].Button.onClick
                .AddListener(() => ApplyReward(GameData.Instance.BrawlPassRewards[i1]));
        }

        UpdateMenuButton();
    }

    public void DebugLog()
    {
        PlayerData.Instance.Save.BoxesUnlocked++;
        UpdateMenuButton();
    }

    public void ApplyReward(BrawlPassReward reward)
    {
        if (reward.RewardType == RewardType.Coins)
        {
            PlayerData.Instance.Save.Coins += reward.Value;
        }
        else if (reward.RewardType == RewardType.Gems)
        {
            PlayerData.Instance.Save.Gems += reward.Value;
        }
        else if (reward.RewardType == RewardType.Brawler && !reward.Skin)
        {
            PlayerData.Instance.Save.Brawlers[reward.Value].Unlocked = true;
        }
        else if (reward.RewardType == RewardType.Brawler && reward.Skin)
        {
            PlayerData.Instance.Save.Brawlers[reward.Value].Unlocked = true;
            PlayerData.Instance.Save.Brawlers[reward.Value].SkinUnlocked = true;
        }

        PlayerData.Instance.Save.BrawlPassRewardClaimed[GameData.Instance.BrawlPassRewards.IndexOf(reward)] = true;
        UpdateButtons();
    }

    public void UpdateMenuButton()
    {
        if (BrawlPassLevel < GameData.Instance.BrawlPassRewards.Count)
        {
            _brawlPassLevelText.text = (1 + BrawlPassLevel).ToString();
            _brawlPassProgressText.text = $"{PlayerData.Instance.Save.BoxesUnlocked % BOXES_TO_UNLOCK_LEVEL}/15";
            _mainMenuSlider.value = PlayerData.Instance.Save.BoxesUnlocked % BOXES_TO_UNLOCK_LEVEL;
        }
        else
        {
            _brawlPassLevelText.text = GameData.Instance.BrawlPassRewards.Count.ToString();
            _brawlPassProgressText.text = "АВСЬОУЖЕ";
            _mainMenuSlider.value = _mainMenuSlider.maxValue;
        }
    }

    public void UpdateButtons()
    {
        for (int i = 0; i < GameData.Instance.BrawlPassRewards.Count; i++)
        {
            _brawlPassRewardViews[i].Checkmark
                .SetActive(i <= BrawlPassLevel && PlayerData.Instance.Save.BrawlPassRewardClaimed[i]);
            _brawlPassRewardViews[i].Button.interactable =
                i <= BrawlPassLevel && !PlayerData.Instance.Save.BrawlPassRewardClaimed[i];
        }
    }


    public void OpenBrawlPass()
    {
        _brawlPassMenuSlider.value = PlayerData.Instance.Save.BoxesUnlocked;
        UpdateButtons();
    }
}