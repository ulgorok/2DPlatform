using UnityEngine;

public class LightEnemy_Attack : MonoBehaviour
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

    private void Start()
    {
        anim = GetComponent<Animator>();

        enemyCollider = GetComponent<Collider2D>();

        GameObject player = GameObject.FindWithTag("Player");
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
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight() && cooldownTimer >= attackCooldown && !attackInProgress)
        {
            attackInProgress = true;
            cooldownTimer = 1f;
            anim.SetTrigger("Light_Attack");
        }
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



