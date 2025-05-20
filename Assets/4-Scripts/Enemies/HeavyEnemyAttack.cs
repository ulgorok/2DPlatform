using UnityEngine;

public class HeavyEnemyAttack : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown = 1f; // istersen cooldown ekleyebilirsin
    [SerializeField] private float range = 1f;
    [SerializeField] private int damage = 10;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance = 0.5f;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    private Animator anim;
    private Player_Health playerHealth;

    private Collider2D enemyCollider;
    private Collider2D playerCollider;

    private float cooldownTimer = 0f;

    public bool attackInProgress;
    public float distance;
    public float absDistance;
    public GameObject player;
    private void Start()
    {
        anim = GetComponent<Animator>();

        enemyCollider = GetComponent<Collider2D>();

        player = GameObject.Find("Player");
        if (player != null)
        {
            playerCollider = player.GetComponent<Collider2D>();
            if (playerCollider != null && enemyCollider != null)
            {
                // Player ile düşmanın fiziksel çarpışmasını engelle
                Physics2D.IgnoreCollision(playerCollider, enemyCollider);
            }
        }
    }

    private void Update()
    {

        //WTTTFFFF is wrong with this
        distance = player.transform.position.x - transform.position.x;
        absDistance = Mathf.Abs(distance);
        Debug.Log(absDistance);
        if (absDistance <= 0.5f && cooldownTimer >= attackCooldown && !attackInProgress)
        {
            Debug.Log("yay");
            attackInProgress = true;
            cooldownTimer = 1f;
            anim.SetBool("Heavy_Attack", true);
        }

        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCooldown)
        {
            attackInProgress = false;
            anim.SetBool("Heavy_Attack", false);
        }
        //if (PlayerInSight() && cooldownTimer >= attackCooldown && !attackInProgress)
        //{
        //    attackInProgress = true;
        //    cooldownTimer = 1f;
        //    anim.SetTrigger("Heavy_Attack");
        //}
    }

    private bool PlayerInSight()
    {
        // Düşmanın bulunduğu pozisyondan sağa doğru boxcast yapıyoruz
        Vector2 boxCastOrigin = (Vector2)transform.position + Vector2.right * range * colliderDistance;

        float boxWidth = 1f * range;
        float boxHeight = 1f;

        RaycastHit2D hit = Physics2D.BoxCast(boxCastOrigin, new Vector2(boxWidth, boxHeight), 0f, Vector2.left, 0f, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Player_Health>();
            return !playerHealth.dead;
        }

        playerHealth = null;
        attackInProgress = false;
        return false;
    }

    // Bu fonksiyonu animasyon event'ine ekle
    private void DamagePlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        attackInProgress = false;
    }

    private void OnAttackExit()
    {
        attackInProgress = false;
    }

    private void OnDrawGizmosSelected()
    {
        // BoxCast alanını sahnede görsel olarak göstermek için
        Gizmos.color = Color.red;
        Vector2 boxCastOrigin = (Vector2)transform.position + Vector2.right * range * colliderDistance;
        Vector3 size = new Vector3(1f * range, 1f, 1f);
        Gizmos.DrawWireCube(boxCastOrigin, size);
    }
}