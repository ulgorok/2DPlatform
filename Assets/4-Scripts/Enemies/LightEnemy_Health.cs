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

    private Animator anim;
    public GameObject bloodEffect;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        anim.SetBool("Walk", true);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Before Ok");
        if(other.gameObject.CompareTag("PlayerAttack"))
        {
            Debug.Log("Ok");
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
    void Update()
    {
        //if (dazedTime <= 0)
        //{
        //    speed = 5;
        //}
        //else
        //{
        //    speed = 0;
        //    dazedTime -= Time.deltaTime;
        //}
        if(colorTime > 0) 
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
