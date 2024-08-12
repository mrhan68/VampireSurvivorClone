using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuControl : MonoBehaviour
{
    GameManager GameManager;
    public TextMeshProUGUI _hpText;
    public TextMeshProUGUI _damageText;
    public TextMeshProUGUI _speedText;
    public TextMeshProUGUI _shootRateText;
    public TextMeshProUGUI _magneticDistanceText;
    public TextMeshProUGUI _durabilityText;
    public TextMeshProUGUI _expMultiplierText;
    public TextMeshProUGUI _shootNumberText;
    public TextMeshProUGUI _pierceNumberText;
    public TextMeshProUGUI _critChance;
    public TextMeshProUGUI _critDamage;
    public TextMeshProUGUI _dodgeChance;
    public TextMeshProUGUI _luck;
    public TextMeshProUGUI _uniqueUpgrade1;
    public TextMeshProUGUI _uniqueUpgrade2;
    public TextMeshProUGUI _uniqueUpgrade3;
    public TextMeshProUGUI _uniqueUpgrade4;
    void Start(){
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update(){
        _hpText.text = "HP: " + GameManager._maxPlayerHealth;
        _damageText.text = "Damage: " + GameManager.damage;
        _speedText.text = "Speed: " + GameManager._playerSpeed;
        _shootRateText.text = "Shoot Rate: " + 1/GameManager._shootRate;
        _magneticDistanceText.text = "Magnetic Distance: " + GameManager.magneticDistance;
        _durabilityText.text = "Durability: " + GameManager._durability;
        _expMultiplierText.text = "Exp Multiplier: " + GameManager._expMultiplier;
        _shootNumberText.text = "Shoot Number: " + GameManager.numberShoots;
        _pierceNumberText.text = "Pierce Number: " + GameManager.pierceNumber;
        _critChance.text = "Crit Chance: " + GameManager.critChance;
        _critDamage.text = "Crit Damage: " + GameManager.critDamage;
        _dodgeChance.text = "Dodge Chance: " + GameManager.dodgeChance;
        _luck.text = "Luck: " + GameManager.luck;
        _uniqueUpgrade1.text = "Unique Upgrade 1: " + GameManager._uniqueUpgrade1;
        _uniqueUpgrade2.text = "Unique Upgrade 2: " + GameManager._uniqueUpgrade2;
        _uniqueUpgrade3.text = "Unique Upgrade 3: " + GameManager._uniqueUpgrade3;
        _uniqueUpgrade4.text = "Unique Upgrade 4: " + GameManager._uniqueUpgrade4;
    }
    public void Resume(){
        Time.timeScale=1;
        GameManager.isPaused=false;
        GameManager.PauseMenu.SetActive(false);
    }
    public void Restart(){
        Time.timeScale=1;
        GameManager.isPaused=false;
        GameManager.PauseMenu.SetActive(false);
        GameManager.UpgradePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void showMainMenu(){
        Time.timeScale=0;
        GameManager.isPaused=true;
        GameManager.PauseMenu.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
