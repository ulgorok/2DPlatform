using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=Ci1KWAjfL1I 

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private float timeBtwAttacks = 2f;

    private float shootTimer;

    private Rigidbody2D bulletRB;
    private EnemyProjectile enemyProjectile;
    private Collider2D collision;

    private void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= timeBtwAttacks)
        {
            shootTimer = 0;
            //shootTimer();
        }

    }

    private void Shoot()
    {
        //spawn a bullet
        bulletRB = Instantiate(bulletPrefab, transform.position, transform.rotation);

        //set the bullet's velocity
        bulletRB.velocity = bulletRB.transform.right * bulletSpeed;

        //grab a reference to enemyProjectile
        enemyProjectile = bulletRB.gameObject.GetComponent<EnemyProjectile>();

        //set EnemyColl
        enemyProjectile.EnemyColl = collision;
    }

    public Vector2 GetShootDirection()
    {
        Transform playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

        return (playerTrans.position - transform.position).normalized;
    }

}
