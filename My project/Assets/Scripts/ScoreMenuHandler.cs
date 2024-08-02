using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreMenuHandler : MonoBehaviour
{
    GameManager _gameManager;
    public TextMeshProUGUI _killPoint;
    public TextMeshProUGUI _levelText;
    public TextMeshProUGUI _timeSurvived;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _killPoint.text = "Kills: " + _gameManager._killCount;
        _levelText.text = "Level: " + _gameManager._level;
        _timeSurvived.text = "Time Survived: " + Mathf.FloorToInt(_gameManager._timeSurvived) + "s";
    }
}
