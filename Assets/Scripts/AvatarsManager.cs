using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarsManager : MonoBehaviour
{

    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _avatarPanel;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private Sprite[] _avatars;
    [SerializeField] private Image _avatarImage;
    
    public void Start()
    {
        for (int i = 0; i < _avatars.Length; i++)
        {
            var obj = Instantiate(_prefab, _parent);
            obj.GetComponent<Image>().sprite = _avatars[i];
            var i1 = i;
            obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                PlayerData.Instance.Save.AvatarID = i1;
                _avatarPanel.SetActive(false);
                _mainPanel.SetActive(true);
                _avatarImage.sprite = _avatars[PlayerData.Instance.Save.AvatarID];
            });
        }
        _avatarImage.sprite = _avatars[PlayerData.Instance.Save.AvatarID];
    }
}
