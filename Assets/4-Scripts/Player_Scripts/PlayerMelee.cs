using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    private float timeBtwAttack;
    private float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public Animator playerAnim;
    public float attackRange;
    public int damage;
    public float attackRangeX;
    public float attackRangeY;

    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            //then you can attack
            if(Input.GetKey(KeyCode.Mouse0))
            {
                playerAnim.SetTrigger("attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0) ;
                for (int i = 0; i < enemiesToDamage.Length; i++) 
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                    }
            }

            timeBtwAttack = startTimeBtwAttack;
        } 
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

    }
}
