using UnityEngine;
using System.Collections;

public class Player_Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    public float currentShieldHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private float DeathTimer;
    public GameObject ShieldHealthShow;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Update()
    {
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
    }

    private void Awake()
    {
        currentShieldHealth = 0;
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyAttack") && currentShieldHealth == 0)
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("EnemyAttack") && currentShieldHealth > 0)
        {
            TakeShieldDamage(1);
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ShieldHealth"))
        {
            currentShieldHealth++;
            ShieldHealthShow.SetActive(true);
            Destroy(other.gameObject);
        }
    }
    public void TakeShieldDamage(float _damage)
    {
        if (invulnerable) return;
        currentShieldHealth = Mathf.Clamp(currentShieldHealth - _damage, 0, 3);
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        //if (currentHealth > 0)
        //{
        //    anim.SetTrigger("hurt");
        //    StartCoroutine(Invunerability());
        //    //SoundManager.instance.PlaySound(hurtSound);
        //}
        //else
        {
            if (currentHealth <= 0 && !dead)
            {
                Respawn();
                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("grounded", true);
                anim.SetTrigger("Player_Death");

                dead = true;
                //SoundManager.instance.PlaySound(deathSound);
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
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
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
        AddHealth(startingHealth);
        anim.ResetTrigger("Player_Death");
        StartCoroutine(Invunerability());

        //Activate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;

        DeathTimer = 1.15f;


    }



}