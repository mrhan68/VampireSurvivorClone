using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _gunEnemy;
    [SerializeField] GameObject _boss;
    GameManager _gameManager;
    [SerializeField] GameObject[] _spawnPoints;
    // Start is called before the first frame update
    public float _spawnRate = 3f;
    public float _gunSpawnRate = 6f;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnGunEnemy());
        StartCoroutine(SpawnBoss());
        StartCoroutine(SpawnRateIncrease());
    }

    // Update is called once per frame
    IEnumerator SpawnEnemy()
    {
        int nextSpawnLocation = Random.Range(0, _spawnPoints.Length);
        Instantiate(_enemy, _spawnPoints[nextSpawnLocation].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(_spawnRate);
        if(!_gameManager.isGameOver)
            StartCoroutine(SpawnEnemy());
    }
    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(30f);
        int nextSpawnLocation = Random.Range(0, _spawnPoints.Length);
        Instantiate(_boss, _spawnPoints[nextSpawnLocation].transform.position, Quaternion.identity);
        _gameManager._bossHealth += 700;
        _gameManager._bossDamage += 20f;
        yield return new WaitForSeconds(30f);
        if(!_gameManager.isGameOver)
            StartCoroutine(SpawnBoss());
    }
    IEnumerator SpawnGunEnemy(){
        yield return new WaitForSeconds(_gunSpawnRate);
        int nextSpawnLocation = Random.Range(0, _spawnPoints.Length);
        Instantiate(_gunEnemy, _spawnPoints[nextSpawnLocation].transform.position, Quaternion.identity);
        if(!_gameManager.isGameOver)
            StartCoroutine(SpawnGunEnemy());
    }

    IEnumerator SpawnRateIncrease()
    {
        yield return new WaitForSeconds(10f);
        if(_spawnRate>=0.5) {
            _spawnRate -= 0.1f;
            _gunSpawnRate -= 0.1f;
        }
        _gameManager._enemyHealth += 10;
        _gameManager.enemyDamage += 3;
        _gameManager.gunEnemyDamage += 2;
        StartCoroutine(SpawnRateIncrease());
    }
}
