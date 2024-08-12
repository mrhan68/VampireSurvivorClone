using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GunEnemyBehaviour : MonoBehaviour{
    public GameObject bulletPrefab;
    GameManager _gameManager;
    GameObject _player;
    [SerializeField] GameObject _exp;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _popUpDamage;
    float _health;
    float _speed;
    private float _dameReceived=1;
    Quaternion _targetRotation;
    bool _disableEnemy = false;
    Vector2 _moveDirection;
    bool isColded=false;
    private bool isPoisoned = false;
    private float poisonDuration = 5f; // Duration of the poison effect
    private float poisonInterval = 0.75f;
    public float explosionDamage = 30f; // Damage dealt by the explosion
    public float explosionRadius = 5f;  // Radius of the explosion
    float gunRange = 11f;
    private int coldDuration;
    private bool isTrap;
    private int trapDuration;

    void Start(){
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.Find("Player");
        _health = _gameManager._enemyHealth;
        _speed = _gameManager._gunEnemySpeed;
        StartCoroutine (Shoot());
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
        float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
       if(distanceToPlayer > gunRange){
        // Move towards the player if the enemy is further away than the desired distance
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
        } else if(distanceToPlayer < gunRange){
            // Optional: Move away from the player if too close, can be adjusted or removed based on desired behavior
            Vector2 moveAwayDirection = (transform.position - _player.transform.position).normalized;
            transform.position += (Vector3)moveAwayDirection * _speed * Time.deltaTime;
        }
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
        _speed=_gameManager._gunEnemySpeed;
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
        _speed=_gameManager._gunEnemySpeed;
        isTrap = false;
    }
    public void Trapped(){
        trapDuration = 3;
        StartCoroutine(Trap());
    }    
    IEnumerator Shoot()
    {
        while (!_gameManager.isGameOver) // Keep shooting until the game is over
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
            if (distanceToPlayer <= 13f) // Only shoot if within range
            {
                GameObject bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
                Vector2 directionToPlayer = (_player.transform.position - transform.position).normalized;
                bullet.GetComponent<Rigidbody2D>().velocity = directionToPlayer * 8f; // Adjust speed as necessary
                Destroy(bullet, 5f);
                yield return new WaitForSeconds(3f);
            }
            else
            {
                yield return new WaitForSeconds(0.5f); // Wait before checking again
            }
        }
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
