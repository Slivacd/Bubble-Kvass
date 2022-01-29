using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopSkinView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    
    public void Initialize(BrawlerSkin skin, UnityAction action)
    {
        _name.text = skin.SkinName;
        _price.text = skin.Price.ToString();
        _image.sprite = skin.Skin;
        _button.onClick.AddListener(action);
    }
}
