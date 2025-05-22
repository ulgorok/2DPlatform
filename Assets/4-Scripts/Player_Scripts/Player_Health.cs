using UnityEngine;
using System.Collections;

public class Player_Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;

    public CameraShake cameraShake;

    public float currentHealth { get; private set; }
    public float currentShieldHealth { get; private set; }
    private Animator anim;
    public bool dead { get; private set; }
    private float DeathTimer;
    public GameObject ShieldHealthShow;

    //[Header("iFrames")]
    //[SerializeField] private float iFramesDuration;
    //[SerializeField] private int numberOfFlashes;
    public float invulnerabilityDuration = 0.4f;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    public GameObject _tuto;
    private PlayerRespawn playerRespawn;
    public int health; //
    private int damage; //
    private float colorTime; //

    private void Awake()
    {
        _tuto = GameObject.Find("TutorialManager");
        currentShieldHealth = 0;
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    private void Update()
    {
        if (TutoGone.gone == true)
        {
            _tuto.SetActive(false);
        }
        if (DeathTimer > 0)
        {
            DeathTimer -= Time.deltaTime;
            if (DeathTimer <= 0)
            {
                transform.position = new Vector3(-7.51f, 25.246f, -0.57f);
                currentHealth = startingHealth;
                dead = false;
                anim.Play("Player_Respawn");
                DeathTimer = 0;

            }
        }
        //if(iFramesDuration > 0)
        //{
        //    iFramesDuration -= Time.deltaTime;
        //    if(iFramesDuration <= 0)
        //    {
        //        gameObject.layer = LayerMask.NameToLayer("Player");
        //        iFramesDuration = 0;
        //    }
        //}
    }

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("EnemyAttack") && currentShieldHealth > 0)
    //    {
    //        TakeShieldDamage(1);
    //    }
    //}
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ShieldHealth"))
        {
            currentShieldHealth++;
            ShieldHealthShow.SetActive(true);
            Destroy(other.gameObject);
        }
    }
    //public void TakeShieldDamage(float _damage)
    //{
    //    if (invulnerable) return;
    //    currentShieldHealth = Mathf.Clamp(currentShieldHealth - _damage, 0, 3);
    //}
    public void TakeDamage(float _damage)
    {
        if (invulnerable || dead) return;

        anim.SetTrigger("Player_Hurt");
        health -= damage;
        colorTime = 0.35f; //
        StartCoroutine(Invunerability());

        float remainingDamage = _damage;

        if (currentShieldHealth > 0)
        {
            float shieldDamage = Mathf.Min(currentShieldHealth, remainingDamage);
            currentShieldHealth -= shieldDamage;
            remainingDamage -= shieldDamage;

            // Shield hasarı varsa sadece kamera sarsıntısı göster
            cameraShake.ShakerCamera();
        }

        if (remainingDamage > 0)
        {
            currentHealth = Mathf.Clamp(currentHealth - remainingDamage, 0, startingHealth);

            if (currentHealth <= 0 && !dead)
            {
                cameraShake.ShakerCamera(); // Kamera sarsıntısı burada da tetiklenebilir
                DeathTimer = 1.15f;

                Respawn();

                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("grounded", true);
                anim.SetTrigger("Player_Death");

                dead = true;
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    public void AddBlueHealth(float _value)
    {
        currentHealth = currentHealth + _value;
    }
    private IEnumerator Invunerability()
    {
        Debug.Log("START INVULNERABILITY");
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        this.gameObject.layer = LayerMask.NameToLayer("Invincible");

        yield return new WaitForSeconds(invulnerabilityDuration);
        //for (int i = 0; i < numberOfFlashes; i++)
        //{
        //    spriteRend.color = new Color(1, 0, 0, 0.5f);
        //    yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        //    spriteRend.color = Color.white;
        //    yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        //}
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }



    //Respawn
    public void Respawn()
    {
        TutoGone.gone = true;
        AddHealth(startingHealth);
        anim.ResetTrigger("Player_Death");
        StartCoroutine(Invunerability());

        //Activate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;

        playerRespawn.Respawn();
    }



}