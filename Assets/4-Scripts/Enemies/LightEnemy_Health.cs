using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemy_Health : MonoBehaviour
{
    public static int health;
    public float speed;
    private float dazedTime;
    private float colorTime;
    public float startDazedTime;
    public SpriteRenderer _sprite;
    public Rigidbody2D _rigidbody;
    public GameObject _player;
    public float stompForcex;
    public float stompForcey;
    float normdir;

    private Animator anim;
    public GameObject bloodEffect;

    void Start()
    {
        health = 3;
        _player = GameObject.Find("Player");
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        anim.SetBool("Walk", true);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PlayerAttack"))
        {
            TakeDamage(1);
        }
        else if(other.gameObject.CompareTag("StrongPlayerAttack"))
        {
            TakeDamage(2);
        }
        else if(other.gameObject.CompareTag("Stomp"))
        {
            TakeDamage(1);
            _rigidbody.AddForce(new Vector2(-normdir * stompForcex, stompForcey), ForceMode2D.Impulse);
            _rigidbody.velocity = new Vector2(Mathf.Clamp(_rigidbody.velocity.x, -5, 5), _rigidbody.velocity.y);
        }
    }
    void Update()
    {
        float direction = _player.transform.position.x - transform.position.x;
        if (direction < 0)
        {
            normdir = -1;
        }
        else if (direction > 0)
        {
            normdir = 1;
        }
        //if (dazedTime <= 0)
        //{
        //    speed = 5;
        //}
        //else
        //{
        //    speed = 0;
        //    dazedTime -= Time.deltaTime;
        //}
        if (colorTime > 0) 
        {
            colorTime -= Time.deltaTime;
            if(colorTime <= 0 )
            {
                _sprite.color = Color.white;
                colorTime = 0;
            }
        }
        if (health <= 0)
        {
            anim.Play("Light_Death");
            Destroy(gameObject, 1.5f);
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        //dazedTime = startDazedTime;
        _sprite.color = Color.black;
        health -= damage;
        Debug.Log("damage TAKEN !");
        colorTime = 0.35f;
    }

}
