using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleModeManager : MonoBehaviour
{
    [SerializeField] private BattleMode[] _battleModes;
    [SerializeField] private Image _currentModeImage;
    private BattleMode _currentMode;

    private void Start()
    {
        SetCurrentMode(0);
    }

    private void SetCurrentMode(int index)
    {
        _currentMode = _battleModes[index];
        _currentModeImage.sprite = _currentMode.BattleSprite;
    }

    public void Play()
    {
        _currentMode.BattleModeBase.Play();
    }

    [Serializable]
    class BattleMode
    {
        public BattleModeBase BattleModeBase;
        public Sprite BattleSprite;
    }
}


