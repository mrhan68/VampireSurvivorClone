using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    GameManager _gameManager;
    GameObject _player;
    [SerializeField] GameObject _exp;
    [SerializeField] GameObject _popUpDamage;
    float _dameReceived=1;
    float _health;
    float _speed;
    Quaternion _targetRotation;
    bool _disableEnemy = false;
    Vector2 _moveDirection;
    bool isColded=false;
    bool isTrap=false;
    private bool isPoisoned = false;
    private float poisonDuration = 5f; 
    private float coldDuration = 5f;
    private float trapDuration = 3f;
    private float poisonInterval = 0.75f; // Interval between poison damage ticks
    public float explosionDamage = 30f; // Damage dealt by the explosion
    public float explosionRadius = 5f;  // Radius of the explosion

    void Start(){
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.Find("Player");
        _health = _gameManager._enemyHealth;
        _speed = _gameManager._enemySpeed;
    }
    void Update(){
        if(_gameManager.isGameOver) return;
        if(!_gameManager.isGameOver || !_disableEnemy){
            MoveEnemy();
            RotateEnemy();
        }
    }

    void MoveEnemy(){
        if(_gameManager.isGameOver) return;
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
    }

    void RotateEnemy(){
        _moveDirection = _player.transform.position - transform.position;
        _moveDirection.Normalize();
        _targetRotation = Quaternion.LookRotation(Vector3.forward, _moveDirection);

        if(transform.rotation != _targetRotation){
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, 200f * Time.deltaTime);
        }
    }
    public bool TakeDamage(float damage){
        damage*= _dameReceived;
        _health -= damage;
        GameObject popUp = Instantiate(_popUpDamage, transform.position, quaternion.identity);
        popUp.GetComponent<TextMeshPro>().text = damage.ToString();
        popUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
        Destroy(popUp, 1f);
        if(_health <= 0){
            _disableEnemy = true;
            Destroy(this.gameObject);
            Instantiate(_exp, transform.position, quaternion.identity);
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            Vector2 awayFromPlayer = transform.position - collision.transform.position;
            awayFromPlayer.Normalize();

            // Apply a force to move the enemy away from the player
            GetComponent<Rigidbody2D>().AddForce(awayFromPlayer * _gameManager.pushDistance); // Adjust the force magnitude as needed
        }
    }
    private IEnumerator Poison()
    {
        if (isPoisoned)
        {
            yield break; // Exit if already poisoned to prevent recursion
        }

        isPoisoned = true;
        float damage;
        while (poisonDuration>0)
        {
            damage = _health*3/100;
            if(damage<1) damage=1;
            TakeDamage(damage); // Apply poison damage

            // Wait for the next poison tick
            yield return new WaitForSeconds(poisonInterval);
            poisonDuration -= poisonInterval;
        }

        isPoisoned = false;
    }
    public void Poisoned(){
        poisonDuration = 5f;
        StartCoroutine(Poison());
    }
    private IEnumerator Cold()
    {
        if (isColded)
        {
            yield break;
        }

        isColded = true;
        while (coldDuration>0)
        {
            _speed=(_gameManager._enemySpeed*7)/10;
            _dameReceived=1.5f;
            yield return new WaitForSeconds(1);
            coldDuration -= 1;
        }
        _speed=_gameManager._enemySpeed;
        isColded = false;
    }
    public void Colded(){
        coldDuration = 5f;
        StartCoroutine(Cold());
    }
    private IEnumerator Trap()
    {
        if (isTrap)
        {
            yield break; // Exit if already poisoned to prevent recursion
        }

        isTrap = true;
        while (trapDuration>0)
        {
            _speed=0;
            yield return new WaitForSeconds(1);
            trapDuration -= 1;
        }

        isTrap = false;
        _speed=_gameManager._enemySpeed;
    }
    public void Trapped(){
        trapDuration = 3f;
        StartCoroutine(Trap());
    }
    void OnDestroy(){
        if(_gameManager.explode){
            Explode();
        }
        _gameManager._killCount++;
    }
    private void Explode()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("enemy") && hitCollider.gameObject != gameObject)
            {
                EnemyBehaviour enemy = hitCollider.GetComponent<EnemyBehaviour>();
                if (enemy != null)
                {
                    enemy.TakeDamage(explosionDamage);
                }
            }
        }
    }
    
}
