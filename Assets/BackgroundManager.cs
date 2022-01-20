using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private BackgroundButtonView _prefab;
    [SerializeField] private Image _background;
    private List<BackgroundButtonView> _backgroundButtonViews;

    void Start()
    {
        _backgroundButtonViews = new List<BackgroundButtonView>();
        for (int i = 0; i < GameData.Instance.Backgrounds.Length; i++)
        {
            _backgroundButtonViews.Add(Instantiate(_prefab,_parent));
            _backgroundButtonViews[i].Image.sprite = GameData.Instance.Backgrounds[i].Sprite;
            _backgroundButtonViews[i].Text.text = GameData.Instance.Backgrounds[i].Price.ToString();
            _backgroundButtonViews[i].Text.gameObject.SetActive(!PlayerData.Instance.Save.Backgrounds[i]);
            var i1 = i;
            _backgroundButtonViews[i].Button.onClick.AddListener(() =>
            {

                if (!PlayerData.Instance.Save.Backgrounds[i1] && PlayerData.Instance.Save.Gems >= GameData.Instance.Backgrounds[i1].Price)
                {
                    PlayerData.Instance.Save.Gems -= GameData.Instance.Backgrounds[i1].Price;
                    PlayerData.Instance.Save.Backgrounds[i1] = true;
                    _backgroundButtonViews[i1].Text.gameObject.SetActive(false);
                }
                if (PlayerData.Instance.Save.Backgrounds[i1])
                {
                    PlayerData.Instance.Save.BackgroundID = i1;
                    ApplyBackground();
                    UpdateCheckmark();
                }
            });
        }
        ApplyBackground();
        UpdateCheckmark();
    }

    private void ApplyBackground()
    {
        _background.sprite = GameData.Instance.Backgrounds[PlayerData.Instance.Save.BackgroundID].Sprite;
    }

    private void UpdateCheckmark()
    {
        foreach (var backgroundButtonView in _backgroundButtonViews)
        {
            backgroundButtonView.Checkmark.SetActive(false);
        }
        _backgroundButtonViews[PlayerData.Instance.Save.BackgroundID].Checkmark.SetActive(true);
    }
}

[Serializable]
public class Background
{
    public Sprite Sprite;
    public int Price;
}