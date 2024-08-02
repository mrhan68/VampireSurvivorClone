using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{   
    GameManager _gameManager;
    public TextMeshProUGUI _healthText;
    Slider _healthBar;
    void Start()
    {
        _healthBar = GetComponent<Slider>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(_gameManager._maxPlayerHealth!=_healthBar.maxValue)
        {
            SetMaxHealth(_gameManager._maxPlayerHealth);
        }
        if(_gameManager._playerHealth!=_healthBar.value)
        {
            SetHealth(_gameManager._playerHealth);
        }
        _healthText.text = _gameManager._playerHealth + " / " + _gameManager._maxPlayerHealth;
    }
    public void SetMaxHealth(float health)
    {
        _healthBar.maxValue = health;
        _healthBar.value = health;
    }

    public void SetHealth(float health)
    {
        _healthBar.value = health;
    }
}
