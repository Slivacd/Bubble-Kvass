using System;
using System.Collections;
using System.Collections.Generic;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
#if UNITY_IOS
using AppodealAppTracking.Unity.Api;
using AppodealAppTracking.Unity.Common;
#endif
using UnityEngine;

public class AppodealAdvertisment : IAdvertismentNetwork, IRewardedVideoAdListener
#if UNITY_IOS
,IAppodealAppTrackingTransparencyListener
#endif
{
    public Action Reward { get; set; }
    public bool IsInterstitialReady => Appodeal.isLoaded(Appodeal.INTERSTITIAL);
    public bool IsRewardedReady => Appodeal.isLoaded(Appodeal.REWARDED_VIDEO);
    private const string ANDROID_KEY = "a5d7a289cb4b5790b44c2fe55765b0c645b87c1bab11cf07",
        IOS_KEY = "9499f9d9f7c222b674d4d9b8d735884fc9138e67c445b8bd";

    public void Initialize()
    {
        Appodeal.setLogLevel(Appodeal.LogLevel.Verbose);
        Appodeal.disableLocationPermissionCheck();
#if UNITY_ANDROID
        Appodeal.initialize(ANDROID_KEY, Appodeal.REWARDED_VIDEO | Appodeal.INTERSTITIAL);
#elif UNITY_IOS && !UNITY_EDITOR
        Appodeal.initialize(IOS_KEY, Appodeal.REWARDED_VIDEO | Appodeal.INTERSTITIAL);
        AppodealAppTrackingTransparency.RequestTrackingAuthorization(this);
#endif

        Appodeal.setRewardedVideoCallbacks(this);
    }

    public void ShowInterstitial()
    {
        ShowAd(Appodeal.INTERSTITIAL);
    }

    public void ShowRewarded()
    {
        ShowAd(Appodeal.REWARDED_VIDEO);
    }


    private void ShowAd(int adtype)
    {
        if (Appodeal.isLoaded(adtype))
            Appodeal.show(adtype);
    }
    
    public void onRewardedVideoLoaded(bool precache)
    {
    }

    public void onRewardedVideoFailedToLoad()
    {
    }

    public void onRewardedVideoShowFailed()
    {
    }

    public void onRewardedVideoShown()
    {
    }

    public void onRewardedVideoFinished(double amount, string name)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            Reward?.Invoke();
        });
    }

    public void onRewardedVideoClosed(bool finished)
    {
    }

    public void onRewardedVideoExpired()
    {
    }

    public void onRewardedVideoClicked()
    {
    }

    public void AppodealAppTrackingTransparencyListenerNotDetermined()
    {
    }

    public void AppodealAppTrackingTransparencyListenerRestricted()
    {
    }

    public void AppodealAppTrackingTransparencyListenerDenied()
    {
    }

    public void AppodealAppTrackingTransparencyListenerAuthorized()
    {
    }
}
