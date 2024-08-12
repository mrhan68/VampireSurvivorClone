using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameManager _gameManager;
    float damage;
    public float pierceNumber;
    public bool isBounce=false;
    public bool isCold=false;
    public bool isPoison=false;
    public bool isHeal=false;
    public bool isTrap=false;
    List <GameObject> enemiesHit = new List<GameObject>(); 

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pierceNumber=_gameManager.pierceNumber;
        damage = _gameManager.damage;
        if(_gameManager.isGiant) damage+=_gameManager._durability*2;
        if(_gameManager.luckyBullet) damage+=_gameManager.luck*2;
        isBounce=_gameManager.isBounce;
        isCold=_gameManager.isCold;
        isPoison=_gameManager.isPoison;
        isHeal=_gameManager.isHeal;
        isTrap=_gameManager.isTrap;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "enemy") return;
        if(enemiesHit.Contains(collision.gameObject)) return;
        enemiesHit.Add(collision.gameObject);
        bool _isDead;
        if (collision.gameObject.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
        {
            if(isHeal && _gameManager._maxPlayerHealth-_gameManager._playerHealth>=5) _gameManager._playerHealth+=5;
            _isDead=enemy.TakeDamage(damage);
            if(isPoison){
               enemy.Poisoned();
            }
            if(isCold){
                enemy.Colded();
            }
            if(isTrap){
                enemy.Trapped();
            }
        }
        if(collision.gameObject.TryGetComponent<GunEnemyBehaviour>(out GunEnemyBehaviour gunEnemy))
        {
            if(isHeal && _gameManager._maxPlayerHealth-_gameManager._playerHealth>=5) _gameManager._playerHealth+=5;
            _isDead=gunEnemy.TakeDamage(damage);
            if(isPoison){
               gunEnemy.Poisoned();
            }
            if(isCold){
                gunEnemy.Colded();
            }
            if(isTrap){
                gunEnemy.Trapped();
            }
        }
        if (collision.gameObject.TryGetComponent<BossBehaviour>(out BossBehaviour boss))
        {
            if(isHeal && (_gameManager._maxPlayerHealth-_gameManager._playerHealth)>=5) _gameManager._playerHealth+=5;
            _isDead=boss.TakeDamage(damage);
            if(isPoison){
                boss.Poisoned();
            }
            if(isCold){
                boss.Colded();
            }
            if(isTrap){
                boss.Trapped();
            }
        }
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        if(pierceNumber<=0){
            Destroy(this.gameObject);
        }
        else{
            if(isBounce){
                GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("enemy");

                // Filter out the enemies that are already in the enemiesHit list
                List<GameObject> remainingEnemies = new List<GameObject>();
                foreach (GameObject Enemy in allEnemies)
                {
                    if (!enemiesHit.Contains(Enemy))
                    {
                        remainingEnemies.Add(Enemy);
                    }
                }

                // Find the closest enemy
                GameObject closestEnemy = null;
                float closestDistance = float.MaxValue;
                foreach (GameObject Enemy in remainingEnemies)
                {
                    float distance = Vector2.Distance(transform.position, Enemy.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = Enemy;
                    }
                }

                // Change the bullet's velocity to go directly to the closest enemy
                if (closestEnemy != null)
                {
                    Vector2 direction = (closestEnemy.transform.position - transform.position).normalized;
                    GetComponent<Rigidbody2D>().velocity = direction * GetComponent<Rigidbody2D>().velocity.magnitude;
                }
                pierceNumber-=2;
                damage-=damage*0.3f;
            }
            else{
                pierceNumber--;
            }
        }

    }
}
