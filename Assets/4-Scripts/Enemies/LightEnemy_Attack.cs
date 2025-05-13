using UnityEngine;

public class LightEnemy_Attack : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    //[SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    //private float cooldownTimer = Mathf.Infinity;

    //References
    private Animator anim;
    private Player_Health playerHealth;
    //private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    //    enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        if(Mathf.Abs(GameObject.Find("Player").transform.position.x - transform.position.x) < 0.25f)
        {
            anim.SetTrigger("Light_Attack");
        }
        //cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        //if (PlayerInSight())
        //{
        //    if (cooldownTimer >= attackCooldown)
        //    {
        //        cooldownTimer = 0;
        //        anim.SetTrigger("Light_Attack");
        //    }
        //}

        //if (enemyPatrol != null)
        //    enemyPatrol.enabled = !PlayerInSight();
    }

    //private bool PlayerInSight()
    //{
    //    RaycastHit2D hit =
    //        Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
    //        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
    //        0, Vector2.left, 0, playerLayer);

    //    if (hit.collider != null)
    //        playerHealth = hit.transform.GetComponent<Player_Health>();

    //    return hit.collider != null;
    //}
    private void OnDrawGizmos()
    {
       // Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            //new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
            playerHealth.TakeDamage(damage);
    }
}


