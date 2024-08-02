using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    GameManager _gameManager;
    float damage;
    public void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        damage = _gameManager.gunEnemyDamage;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _gameManager.DamagePlayer(damage);
            Destroy(this.gameObject);
        }
    }
}
