using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Box")]
public class Box : ScriptableObject
{
    [SerializeField] private int _coinMin, _coinMax;
    [SerializeField] private int _newBrawlerDropChance;
    public Currency Currency;
    public int Price;
    public BoxView Prefab;
    public int GetCoins() => Random.Range(_coinMin, _coinMax);
    public bool NewBrawlerDrop => Random.Range(0, 101) <= _newBrawlerDropChance && !PlayerData.Instance.BrawlersData.Values.All(t => t.Unlocked);
}

public enum Currency
{
    Coins,
    Gems
}