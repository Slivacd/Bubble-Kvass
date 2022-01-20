using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingGamePlayer : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Slider _health;
    [SerializeField] private FallingGame _fallingGame;
    private bool _gameIsEnd;
    
    public void StartGame()
    {
        _health.maxValue = 10;
        _health.value = 10;
        transform.position = new Vector2(0, transform.position.y);
        _gameIsEnd = false;
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && !_gameIsEnd)
        {
            if(Input.mousePosition.x > Screen.width / 2 && transform.position.x < GameData.Instance.Border)
                transform.Translate(Vector3.right * _speed);
            else if(Input.mousePosition.x < Screen.width / 2 && transform.position.x > -GameData.Instance.Border)
                transform.Translate(Vector3.left * _speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            _health.value -= 1;
            other.GetComponent<Image>().enabled = false;
            if (_health.value == 0)
            {
                _fallingGame.EndGame();
                _gameIsEnd = true;
            }
        }

        if (other.CompareTag("Gem"))
        {
            _fallingGame.AddGem();
            other.GetComponent<Image>().enabled = false;
        }
        
        if (other.CompareTag("Coin"))
        {
            _fallingGame.AddCoin();
            other.GetComponent<Image>().enabled = false;
        }
    }
}
