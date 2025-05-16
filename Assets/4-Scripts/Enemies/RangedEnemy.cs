using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    public static bool canFire;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private CapsuleCollider2D capsuleCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = 0f;

    //References
    private Animator anim;
    //private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        canFire = true;
        anim = GetComponent<Animator>();
        firePoint.position = this.gameObject.transform.Find("FirePoint").position;
    }



    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Projectile_Attack");
            }
        }

        //if (enemyPatrol != null)
        //    enemyPatrol.enabled = !PlayerInSight();
    }

    private void RangedAttack()
    {
        if (canFire)
        {

            anim.SetTrigger("Player_Attack");
            Instantiate(bullet, new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z), Quaternion.identity);
            cooldownTimer = 0.7f;
            canFire = false;
        }
        //bullets[0].transform.position = firePoint.position;
        //bullets[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        if (capsuleCollider == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y, capsuleCollider.bounds.size.z));
    }

}