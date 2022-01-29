using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Box")]
public class Box : ScriptableObject
{
    [SerializeField] private int _coinMin, _coinMax;
    [SerializeField] private int _gemMin, _gemMax;
    [SerializeField] private int _newBrawlerDropChance;
    [SerializeField] private int _gemChance;
    public Currency Currency;
    public int Price;
    public BoxView Prefab;
    public int GetCoins() => Random.Range(_coinMin, _coinMax);
    public int GetGems() => Random.Range(_gemMin, _gemMax);
    public bool NewBrawlerDrop => Random.Range(0, 101) <= _newBrawlerDropChance && !PlayerData.Instance.BrawlersData.Values.All(t => t.Unlocked);
    public bool GemDrop => Random.Range(0, 101) <= _gemChance;
}

public enum Currency
{
    Coins,
    Gems
}