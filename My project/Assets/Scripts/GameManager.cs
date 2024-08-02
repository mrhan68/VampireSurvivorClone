using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public EnemySpawn EnemySpawn;
    public TextMeshProUGUI _text;
    public GameObject UpgradePanel;
    public GameObject rerollButton;
    public GameObject PauseMenu;
    public bool isGameOver;
    public float damage;
    public float _shootRate ;
    public float _enemyHealth ;
    public float _bossHealth ;
    public float _expBar;
    public float _level;
    public float magneticDistance;
    public float pushDistance;
    public float _enemySpeed;
    public float _gunEnemySpeed;
    public float _bossDamage;
    public float _playerHealth;
    public float _playerSpeed;
    public float _maxPlayerHealth;
    public float _durability;
    public float enemyDamage;
    public float gunEnemyDamage;
    public float _expMultiplier;
    public bool isPaused;
    public float numberShoots;
    public float pierceNumber;
    public bool explode;
    public bool isBounce;
    public bool isCold;
    public bool isPoison;
    public bool isHeal;
    public bool isTrap;
    public bool isGiant;
    public bool brumbleVest;
    public float _rerollCount;
    public float _timeSurvived;
    public float _killCount;
    public string _uniqueUpgrade1;
    public string _uniqueUpgrade2;
    public string _uniqueUpgrade3;
    public bool inUpgrade;
    public void Start(){
        initial();
        UpgradePanel.SetActive(false);
        PauseMenu.SetActive(false);
        Time.timeScale=1;
    }
    public void Update(){
        if (!isPaused)
        {
            _timeSurvived += Time.deltaTime;
        }
        _text.text = "Level: " + _level;
        if(!inUpgrade && _expBar >= _level*2){
            levelUp();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && !inUpgrade)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void levelUp(){
        _playerHealth+=20;
        _maxPlayerHealth+=10;
        if(_playerHealth>_maxPlayerHealth) _playerHealth=_maxPlayerHealth;
        Time.timeScale=0;
        isPaused=true;
        inUpgrade=true;
        UpgradePanel.SetActive(true);
        rerollButton.SetActive(true);
        _rerollCount=1;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        PauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        PauseMenu.SetActive(false);
    }
    public void DamagePlayer(float damage){
        if(!brumbleVest) _playerHealth -= damage-_durability;
        else _playerHealth -= (damage-_durability)/2;
        if(_playerHealth<=0){
            isGameOver = true;
            Destroy(GameObject.Find("Player"));
        }
    }
    public void initial(){
        isGameOver = false;
        damage = 35;
        _shootRate = 1f;
        _enemyHealth = 100;
        _bossHealth = 1000;
        _expBar=0;
        _level=0;
        magneticDistance = 5f;
        pushDistance = 500f;
        _enemySpeed =4f;
        _gunEnemySpeed = 3f;
        _bossDamage = 50;
        _playerHealth = 90;
        _playerSpeed = 6f;
        _maxPlayerHealth = 90;
        _durability = 0;
        enemyDamage = 20;
        gunEnemyDamage = 10;
        _expMultiplier=1f;
        isPaused = false;
        numberShoots = 1;
        pierceNumber = 0;
        explode = false;
        isBounce=false;
        isCold=false;
        isPoison=false;
        isHeal=false;
        isTrap=false;
        isGiant=false;
        brumbleVest=false;
        EnemySpawn._spawnRate = 3f;
        EnemySpawn._gunSpawnRate = 6f;
        _killCount=0;
        _timeSurvived=0;
    }
}
