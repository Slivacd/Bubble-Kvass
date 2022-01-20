using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdvertismentNetwork
{
    Action Reward { get; set; }
    bool IsInterstitialReady { get; }
    bool IsRewardedReady { get; }

    void Initialize();
    
    void ShowInterstitial();

    void ShowRewarded();

}