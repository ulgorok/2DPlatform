using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=Ci1KWAjfL1I 

public class EnemyProjectile : MonoBehaviour, IDeflectable
{
    [SerializeField] private float damageAmount = 1f;
    [SerializeField] private AnimationCurve speedCurve; 
    private IDamageable iDamageable;

    public Collider2D EnemyColl {  get; set; }

    public float ReturnSpeed { get; set; } = 10f;

    public bool IsDeflecting { get; set; }

    private Collider2D coll;
    private Rigidbody2D rb;

    private float speed, time;

    public void Start()
    {
        //coll = GetComponent<Collider2D>;
        //rb = GetComponent<Rigidbody2D>;

        IgnoreCollisionWithEnemyToggle();
    }

    private void FixedUpdate()
    {
        if (IsDeflecting)
        {
            speed = speedCurve.Evaluate(time);
            time += Time.fixedDeltaTime;

            rb.velocity = transform.right * speed * ReturnSpeed;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        iDamageable = collision.gameObject.GetComponent<IDamageable>();
        if (iDamageable != null )
        {
            iDamageable.Damage(damageAmount);
        }
    }

    private void IgnoreCollisionWithEnemyToggle()
    {
        if (!Physics2D.GetIgnoreCollision(coll, EnemyColl))
        {
            //turn ON ignore collisions
            Physics2D.IgnoreCollision(coll, EnemyColl, true);
        }
        else
        {
            //turn OFF ignore collisions
            Physics2D.IgnoreCollision(coll, EnemyColl, false);
        }
    }

    public void Deflect(Vector2 direction)
    {
        IsDeflecting = true;

        IgnoreCollisionWithEnemyToggle();

        if ((direction.x > 0 && transform.right.x < 0) || (direction.x < 0 && transform.right.x > 0))
        {
            transform.right = -transform.right;
        }

        transform.right = -transform.right;
        rb.velocity = direction * ReturnSpeed;
    }

    internal void ActivateProjectile()
    {
        throw new NotImplementedException();
    }
}
