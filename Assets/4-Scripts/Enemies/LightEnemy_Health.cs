using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemy_Health : MonoBehaviour
{
    public int health;
    public float speed;
    private float colorTime;
    public SpriteRenderer _sprite;
    public Rigidbody2D _rigidbody;
    public GameObject _player;
    public float stompForcex;
    public float stompForcey;
    float normdir;
    public GameObject itemPrefab;


    private Animator anim;
    public GameObject bloodEffect;
    private bool hasDroppedItem = false;

    void Awake()
    {
        health = 3;
        _player = GameObject.Find("Player");
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        anim.SetBool("Light_Walk", true);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            TakeDamage(1);
        }
        else if (other.gameObject.CompareTag("StrongPlayerAttack"))
        {
            TakeDamage(2);
        }
        else if (other.gameObject.CompareTag("Stomp"))
        {
            TakeDamage(1, true);

            _rigidbody.AddForce(new Vector2(-normdir * stompForcex, stompForcey), ForceMode2D.Impulse);
            _rigidbody.velocity = new Vector2(Mathf.Clamp(_rigidbody.velocity.x, -5, 5), _rigidbody.velocity.y);
        }
    }

    void Update()
    {
        float direction = _player.transform.position.x - transform.position.x;
        normdir = direction < 0 ? -1 : 1;

        if (colorTime > 0)
        {
            colorTime -= Time.deltaTime;
            if (colorTime <= 0)
            {
                _sprite.color = Color.white;
                colorTime = 0;
            }
        }

        //if (health <= 0 && !hasDroppedItem)
        //{
        //    anim.Play("Light_Death");
        //    Instantiate(itemPrefab, transform.position + Vector3.up * 0.10f, Quaternion.identity);
        //    hasDroppedItem = true;
        //    gameObject.SetActive(false);
        //}

        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage, bool damagedByStomp = false)
    {
        if (health <= 0) return; // Zaten ölmüşse işlem yapma

        anim.SetTrigger("Light_Hurt");
        health -= damage;
        colorTime = 0.35f;

        if (health <= 0)
        {
            anim.Play("Light_Death");

            // Sadece stomp ile öldürüldüyse item düşsün
            if (damagedByStomp)
            {
                Instantiate(itemPrefab, transform.position + Vector3.up * 0.10f, Quaternion.identity);
            }

            PlayerRespawn.deadEnemies.Add(GetComponent<EnemyRespawnHandler>());
        }
    }

    public void ResetStats()
    {
        health = 3;
        _sprite.color = Color.white;
        anim.Rebind();
        anim.Update(0f);
        anim.SetBool("Walk", true);
        _rigidbody.velocity = Vector2.zero;
        gameObject.SetActive(true);
    }

    private void OnDeathAnimationFinished()
    {
        gameObject.SetActive(false);
    }
}
