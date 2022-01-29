using System;
using UnityEngine;

[Serializable]
public class BrawlPassReward
{
    public RewardType RewardType;
    public int Value;
    public bool Skin => RewardType == RewardType.Brawler && skin;
    [SerializeField] public bool skin;
    
}

public enum RewardType
{
    Coins,
    Gems,
    Brawler
}