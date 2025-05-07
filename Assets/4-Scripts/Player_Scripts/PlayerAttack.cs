using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private List<IDamageable> iDamageables = new List<IDamageable>();
    private List<IDeflectable> iDeflectables = new List<IDeflectable>();
    private int horizontalInput;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        //if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    //https://www.youtube.com/watch?v=Ci1KWAjfL1I

    //public IEnumerator DamageWhileSlashIsActive()
    //{
    //    ShouldBeDamaging = true;

    //    while (ShouldBeDamaging)
    //    {
    //        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);

    //        for (int i = 0; i< hits.Length; i++)
    //        {
    //            //DAMAGE
    //            IDamageable iDamageable = hits[i].collider.gameObjects.GetComponent<iDamageable>();

    //            if (iDamageable != null && !iDamageable.Contains(iDamageable))
    //            {
    //                iDamageable.Damage(damageAmount);
    //                iDamageable.Add(iDamageable);
    //            }

    //            //DEFLECT
    //            IDeflectable iDeflectable = hits[i].collider.gameObject.GetObject.GetComponent<iDeflectable>();

    //            if (iDeflectable != null && !iDeflectable.Contains(iDeflectable))
    //            {
    //                iDeflectable.Deflect(transform.right);
    //                iDeflectable.Add(iDeflectable);
    //            }

    //        }

    //        yield return null;
    //    }

    //    ReturnAttackablesToDamageable();
    //}

    //private void ReturnAttackablesToDamageable()
    //{
    //    iDamageables.Clear();
    //    iDeflectables.Clear();
    //}

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

    private bool onWall()
    {
        throw new NotImplementedException();
    }

    private bool isGrounded()
    {
        throw new NotImplementedException();
    }
}
