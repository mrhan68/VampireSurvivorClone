using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    GameManager _gameManager;
    GameObject _player;
    [SerializeField] GameObject _exp;
    [SerializeField] GameObject _popUpDamage;
    float _health;
    float _speed;
    private float _dameReceived=1;
    Quaternion _targetRotation;
    bool _disableEnemy = false;
    Vector2 _moveDirection;
    Vector3 _playerPosition;
    bool isColded=false;
    private bool isPoisoned = false;
    private float poisonDuration = 5f; // Duration of the poison effect
    private float poisonInterval = 0.75f;
    private int coldDuration;
    private bool isTrap;
    private int trapDuration;
    public float explosionDamage = 30f; // Damage dealt by the explosion
    public float explosionRadius = 5f;  // Radius of the explosion
    private bool dashing = false;

    void Start(){
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.Find("Player");
        _health = _gameManager._bossHealth;
        _speed = _gameManager._enemySpeed;
        StartCoroutine(Dash());
    }
    void Update(){
        if(_gameManager.isGameOver) return;
        if(!dashing) _playerPosition = _player.transform.position; 
        if(!_gameManager.isGameOver || !_disableEnemy){
            MoveEnemy();
            RotateEnemy();
        }
    }

    void MoveEnemy(){
        if(_gameManager.isGameOver) return;
        transform.position = Vector2.MoveTowards(transform.position, _playerPosition, _speed * Time.deltaTime);
    }

    void RotateEnemy(){
        _moveDirection = _playerPosition - transform.position;
        _moveDirection.Normalize();
        _targetRotation = Quaternion.LookRotation(Vector3.forward, _moveDirection);

        if(transform.rotation != _targetRotation){
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, 200f * Time.deltaTime);
        }
    }
    public bool TakeDamage(float damage){
        bool isCrit = false;
        int weight = UnityEngine.Random.Range(0, 100);
        if(weight<_gameManager.critChance){
            damage*=_gameManager.critDamage/100;
            isCrit = true;
        }
        damage*= _dameReceived;
        _health -= damage;
        GameObject popUp = Instantiate(_popUpDamage, transform.position, quaternion.identity);
        popUp.GetComponent<TextMeshPro>().text = damage.ToString();
        popUp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
        if(isCrit){
            popUp.GetComponent<TextMeshPro>().color = Color.red;
        }
        Destroy(popUp, 1f);
        if(_health <= 0){
            _disableEnemy = true;
            Destroy(this.gameObject);
            Instantiate(_exp, transform.position, quaternion.identity);
            return true;
        }
        return false;
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
        coldDuration = 5;
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
        _speed=_gameManager._enemySpeed;
        isTrap = false;
    }
    public void Trapped(){
        trapDuration = 3;
        StartCoroutine(Trap());
    }
    void OnDestroy(){
        if(_gameManager.explode){
            Explode();
        }
        _gameManager._killCount++;
    }
    private IEnumerator Dash()
    {
        float dashRange = 10f; // Distance from the player to start dashing
        float dashTime = 0.5f; // Time taken to dash to the player

        while (true)
        {
            if (Vector3.Distance(transform.position, _player.transform.position) <= dashRange)
            {
                dashing = true;
                _speed=0;
                yield return new WaitForSeconds(1);
                float elapsedTime = 0f;
                _speed=_gameManager._enemySpeed*10;
                while (elapsedTime < dashTime)
                {
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                _speed=_gameManager._enemySpeed;
                dashing = false; 
               yield return new WaitForSeconds(5);
            }
            yield return new WaitForSeconds(0.1f); // Check every 0.1 seconds
        }
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
