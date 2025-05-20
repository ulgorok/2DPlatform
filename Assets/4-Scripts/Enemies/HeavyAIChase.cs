using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAIChase : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private float distance;
    public Animator anim;
    private LightEnemy_Health healthScript;

    void Start()
    {
        anim = GetComponent<Animator>();
        healthScript = GetComponent<LightEnemy_Health>();
    }

    void Update()
    {
        if (healthScript.health > 0)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            if (distance < 5)
            {
                anim.SetBool("Heavy_Walk", true);
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("Heavy_Walk", false);
            }

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }
}

