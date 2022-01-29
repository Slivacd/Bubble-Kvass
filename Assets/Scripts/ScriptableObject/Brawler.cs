using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Brawler")]
public class Brawler : ScriptableObject
{
    public int ID;
    public string Description;
    public string Rarity;
    public Color RarityColor;
    public BrawlerSkin[] Skins;
}

[Serializable]
public class BrawlerSkin
{
    public string SkinName;
    public Sprite Skin;
    public int Price;
}

