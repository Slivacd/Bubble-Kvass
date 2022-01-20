using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingEntity : MonoBehaviour
{

    [SerializeField] private Image _image;
    private float _speed;
    public Action<FallingEntity> EnemyFall;

    public void Initialize(float speed, Sprite sprite)
    {
        _speed = speed;
        _image.sprite = sprite;
    }
    
    void FixedUpdate()
    {
        transform.Translate(Vector3.down * _speed);
        if(transform.position.y < -15)
            EnemyFall?.Invoke(this);
    }
}
