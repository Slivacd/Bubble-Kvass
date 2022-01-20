using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class FallingGame : BattleModeBase
{
    [SerializeField] private Transform _parent;
    [SerializeField] private FallingEntity _prefab;
    [SerializeField] private FallingEntity _gemPrefab, _coinPrefab;
    [SerializeField] private FallingGamePlayer _player;
    [FormerlySerializedAs("_spawnTime")] [SerializeField] private float _startSpawnTime;
    [SerializeField] private Sprite[] _enemySprites;
    [SerializeField] private Sprite _coinSprite, _gemSprite;
    [SerializeField] private float _startSpeed;
    [SerializeField] private TextMeshProUGUI _gemsText, _coinsText;
    [SerializeField] private GameObject _endPanel;
    private int _gems, _coins;
    
    [SerializeField]  private List<FallingEntity> _enemies;
    private float _speed;
    private float _spawnTime;
    private Coroutine _spawner;
    
    public IEnumerator SpawnEnemy()
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        while (true)
        {
            if (Random.Range(0, 10) == 0)
            {
                var gem = Instantiate(_gemPrefab,
                    new Vector2(Random.Range(-GameData.Instance.Border, GameData.Instance.Border), _parent.position.y),
                    Quaternion.identity, _parent);
                gem.Initialize(_speed, _gemSprite);
                gem.transform.localPosition = new Vector3(gem.transform.localPosition.x, gem.transform.localPosition.y, 0);
                gem.EnemyFall += c => Destroy(c.gameObject);
            }
            if (Random.Range(0, 5) == 0)
            {
                var coin = Instantiate(_coinPrefab,
                    new Vector3(Random.Range(-GameData.Instance.Border, GameData.Instance.Border), _parent.position.y,0), Quaternion.identity, _parent);
                coin.Initialize(_speed, _coinSprite);
                coin.transform.localPosition = new Vector3(coin.transform.localPosition.x, coin.transform.localPosition.y, 0);
                coin.EnemyFall += c => Destroy(c.gameObject);
            }
            float xPos = Random.Range(-GameData.Instance.Border, GameData.Instance.Border);
            _enemies.Add(Instantiate(_prefab,new Vector3(xPos,_parent.position.y,0),Quaternion.identity,_parent));
            var enemy = _enemies.Last();
            enemy.transform.localPosition = new Vector3(enemy.transform.localPosition.x, enemy.transform.localPosition.y, 0);
            enemy.Initialize(_speed,_enemySprites[Random.Range(0,_enemySprites.Length)]);
            enemy.EnemyFall += OnEnemyFall;
            _speed = Mathf.Lerp(0.2f, 0.3f, Mathf.InverseLerp(0, 40, stopWatch.Elapsed.Seconds));
            _spawnTime = Mathf.Lerp(1f, 0.6f, Mathf.InverseLerp(0, 40, stopWatch.Elapsed.Seconds));
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    public void AddGem()
    {
        _gems++;
        _gemsText.text = _gems.ToString();
    }

    public void AddCoin()
    {
        _coins++;
        _coinsText.text = _coins.ToString();
    }

    public void EndGame()
    {
        StopCoroutine(_spawner);
        for (int i = 0; i < _enemies.Count; i++)
            Destroy(_enemies[i].gameObject);
        _enemies.Clear();
        _endPanel.SetActive(true);
    }

    private void OnEnemyFall(FallingEntity entity)
    {
        _enemies.Remove(entity);
        Destroy(entity.gameObject);
    }

    public override void Play()
    {
        gameObject.SetActive(true);
        _gems = 0;
        _coins = 0;
        _gemsText.text = _gems.ToString();
        _coinsText.text = _coins.ToString();
        _enemies = new List<FallingEntity>();
        _speed = _startSpeed;
        _spawnTime = _startSpawnTime;
        _spawner = StartCoroutine(SpawnEnemy());
        _endPanel.SetActive(false);
        _player.StartGame();
    }
    
}
