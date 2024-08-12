using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    GameManager _gameManager;
    GameObject closestEnemy;
    public GameObject scoreScreen;
    Rigidbody2D _rb;
    float _moveVertical;
    float _moveHorizontal;
    float _speedLimiter = 0.7f;
    Vector2 _moveVelocity;
    Vector2 offset;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _bulletSpawn;
    
    [SerializeField] GameObject _popUpDamage;
    GameObject[] enemies=null;
    Vector2 _shootDirection;
    float _bulletSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {  
        scoreScreen.SetActive(false);
        StartCoroutine(Shoot());
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();     
    }

    // Update is called once per frame
    void Update()
    {   
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        if(enemies.Length!=0) closestEnemy = FindClosestEnemy();
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        _moveVertical = Input.GetAxisRaw("Vertical");
        _moveVelocity = new Vector2(_moveHorizontal, _moveVertical) * _gameManager._playerSpeed;
    }
    

    void FixedUpdate()
    {
        MovePlayer();
        if(enemies!=null && enemies.Length!=0) Rotate();
    }

    private void Rotate()
    {
        if (closestEnemy != null)
        {
            offset = new Vector2(closestEnemy.transform.position.x - transform.position.x, closestEnemy.transform.position.y - transform.position.y).normalized;

            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle-90f);
        }
    }

    void MovePlayer()
    {
       if(_moveHorizontal != 0 || _moveVertical != 0)
       {
           if(_moveHorizontal != 0 && _moveVertical != 0)
           {
               _moveVelocity *= _speedLimiter;
           }
           _rb.velocity = _moveVelocity;
       }
       else
       {
           _moveVelocity = Vector2.zero;
           _rb.velocity = _moveVelocity;
       }
    }
    IEnumerator Shoot()
    {
        if(enemies==null || Vector2.Distance(transform.position, closestEnemy.transform.position) > 20f){
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(Shoot());
        }
        else{
            for(int i=0;i<_gameManager.numberShoots;i++){
                GameObject bullet = Instantiate(_bullet, _bulletSpawn.transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = offset * _bulletSpeed;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(_gameManager._shootRate);
            if(!_gameManager.isGameOver) StartCoroutine(Shoot());
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 playerPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(playerPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                closest = enemy;
                minDistance = distance;
            }
        }

        return closest;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
        {
            if(_gameManager.brumbleVest){
                enemy.TakeDamage(20);
            }
            takeDamage(_gameManager.enemyDamage);
        }
        if(collision.gameObject.TryGetComponent<BossBehaviour>(out BossBehaviour boss))
        {
            if(_gameManager.brumbleVest){
                boss.TakeDamage(20);
            }
            takeDamage(_gameManager._bossDamage);
        }
        if(collision.gameObject.TryGetComponent<GunEnemyBehaviour>(out GunEnemyBehaviour gunEnemy))
        {
            if(_gameManager.brumbleVest){
                gunEnemy.TakeDamage(20);
            }
            takeDamage(_gameManager.gunEnemyDamage);
        }
        if(_gameManager._playerHealth<=0){
            _gameManager.isGameOver = true;
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemyBullet")
        {
            takeDamage(_gameManager.gunEnemyDamage);
            if(_gameManager._playerHealth<=0){
                _gameManager.isGameOver = true;
                Destroy(this.gameObject);
            }
        }
    }
    private void OnDestroy()
    {
        Time.timeScale = 0;
        scoreScreen.SetActive(true);
    }
    private void takeDamage(float damage)
    {
        damage -=_gameManager._durability;
        if(damage<0) damage=0;
        int weight = UnityEngine.Random.Range(0, 100);
        if(weight<_gameManager.dodgeChance){
            GameObject popUp = Instantiate(_popUpDamage, transform.position, quaternion.identity);
            popUp.GetComponent<TextMeshPro>().text = "Dodge!";
            popUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
            Destroy(popUp, 1f);
        }
        else{
            if(_gameManager.brumbleVest) damage/=2;
            _gameManager._playerHealth -= damage;
            GameObject popUp = Instantiate(_popUpDamage, transform.position, quaternion.identity);
            popUp.GetComponent<TextMeshPro>().text = damage.ToString();
            popUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
            popUp.GetComponent<TextMeshPro>().color = Color.red;
            Destroy(popUp, 1f);
        }
    }
}
