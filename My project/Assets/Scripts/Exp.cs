using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    GameManager _gameManager;
    GameObject _player;
    public float moveSpeed = 10f; // Speed at which the Exp moves towards the player
     // Distance within which the Exp starts moving towards the player
    // Start is called before the first frame update
     void Start(){
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.Find("Player");
     }

     void Update(){
        if(_player!=null && !_gameManager.isGameOver){
            float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
            if (distanceToPlayer <= _gameManager.magneticDistance)
            {
                // Move towards the player
                transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, moveSpeed * Time.deltaTime);
            }
        }
     }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            _gameManager._expBar += 1*_gameManager._expMultiplier;
        }
    }
}
