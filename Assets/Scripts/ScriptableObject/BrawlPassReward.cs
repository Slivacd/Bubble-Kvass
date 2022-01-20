using System;

[Serializable]
public class BrawlPassReward
{
    public RewardType RewardType;
    public int Value;
}

public enum RewardType
{
    Coins,
    Gems,
    Brawler
}