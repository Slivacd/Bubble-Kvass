using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class Advertisment : MonoBehaviour
{
    public static Advertisment Instance;
#pragma warning disable CS0649
    [SerializeField] private GameObject rewardLoadingPanel;
    [SerializeField] private float loadingTime;
#pragma warning restore CS0649

    private IAdvertismentNetwork _advertisment = new AppodealAdvertisment();
    private void Awake() => Instance = this;

    void Start()
    {
        _advertisment.Initialize();
#if !UNITY_EDITOR
        StartCoroutine(InterstitialAd());
#endif
    }

    public void TryRewardedAd(Action reward)
    {
        _advertisment.Reward = reward;

#if !UNITY_EDITOR
        if (GameData.Instance.NetworkAvailable)
            StartCoroutine(LoadRewardedAd());

        IEnumerator LoadRewardedAd()
        {
            float timer = loadingTime;
            rewardLoadingPanel.SetActive(true);
            yield return new WaitUntil(() =>
            {
                timer -= Time.deltaTime;
                return _advertisment.IsRewardedReady || timer <= 0;
            });
            rewardLoadingPanel.SetActive(false);

            if (_advertisment.IsRewardedReady)
                _advertisment.ShowRewarded();
        }

#else
        _advertisment.Reward?.Invoke();
#endif
    }

    private IEnumerator InterstitialAd()
    {
        while (true)
        {
            yield return new WaitForSeconds(160);
            if (!GameData.Instance.NetworkAvailable)
                continue;
            if (_advertisment.IsInterstitialReady)
            {
                _advertisment.ShowInterstitial();
            }
        }
    }
}