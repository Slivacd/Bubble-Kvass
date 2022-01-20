using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Brawler")]
public class Brawler : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;
    public string Rarity;
    public Sprite LockedIcon, UnlockedIcon;
    public BrawlerSkin[] Skins;
}

[Serializable]
public class BrawlerSkin
{
    public string SkinName;
    public Sprite Skin;
}

