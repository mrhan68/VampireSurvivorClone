using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{   
    GameManager _gameManager;
    Slider _healthBar;
    void Start()
    {
        _healthBar = GetComponent<Slider>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleExpBarVisibility();
        }

        UpdateExpBar();
    }

    private void ToggleExpBarVisibility()
    {
        // Toggle the active state of the exp bar's GameObject
        _healthBar.gameObject.SetActive(!_healthBar.gameObject.activeSelf);
    }

    void UpdateExpBar()
    {
        if(_gameManager._level*2!=_healthBar.maxValue)
        {
            SetMaxHealth(_gameManager._level*2);
        }
        if(_gameManager._expBar!=_healthBar.value)
        {
            SetHealth(_gameManager._expBar);
        }
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
