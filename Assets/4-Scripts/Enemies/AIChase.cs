using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=2SXa10ILJms 

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private float distance;
    public Animator anim;
    // Start is called before the first frame update

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    //Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if(distance < 5 )
        {
            anim.SetBool("Light_Walk", true);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Light_Walk", false);
        }
        if(direction.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if(direction.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

}
