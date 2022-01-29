using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopGemView : MonoBehaviour
{
    [SerializeField] private int _id = -1;
    [SerializeField] private int _maxCount;
    [SerializeField] private int _rewardAmount;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        _text.text = $"{PlayerData.Instance.Save.ShopAdCounter[_id]}/{_maxCount}";
    }

    public void TryBuyGem()
    {
        Advertisment.Instance.TryRewardedAd(() =>
        {
            if (++PlayerData.Instance.Save.ShopAdCounter[_id] == _maxCount)
            {
                PlayerData.Instance.Save.Gems += _rewardAmount;
                PlayerData.Instance.Save.ShopAdCounter[_id] = 0;
                Debug.Log("Ad2");
            }
            Setup();
            Debug.Log("Ad1");
        });
    }
}
