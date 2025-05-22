using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] public float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bigBullet;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = 0f;
    private float cooldownStompTimer = 0f;
    public static bool canFire;
    public static int _bulletsLeft;
    public static int weaponChosen;
    public bool canMelee;
    public static bool canStomp;
    public float canMeleeTimer;
    //public Transform _canvas;
    //public Transform _slider;
    public CameraShake cameraShake;
    AudioManager audioManager;  //

    //public Sprite _pistol;
    //public Sprite _shotGun;
    //public Sprite _sword;
    //public Sprite _katana;
    private void Awake()
    {
        canMelee = true;
        canStomp = true;
        weaponChosen = 0;
        _bulletsLeft = 5;
        canFire = true;
        //_canvas = this.gameObject.transform.Find("ReloadCanvas");
        //_slider = _canvas.transform.Find("ReloadSlider");
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        firePoint.position = this.gameObject.transform.Find("FirePoint").position;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); //
    }

    public void Update()
    {
        if (cooldownTimer > 0 && _bulletsLeft > 0)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                canFire = true;
                cooldownTimer = 0;
            }
        }
        if (cooldownStompTimer > 0)
        {
            cooldownStompTimer -= Time.deltaTime;
            if (cooldownStompTimer <= 0)
            {
                canStomp = true;
                cooldownStompTimer = 0;
            }
        }
        if (canMeleeTimer > 0)
        {
            canMeleeTimer -= Time.deltaTime;
            if (canMeleeTimer <= 0)
            {
                canMelee = true;
                canMeleeTimer = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(weaponChosen == 0)
            {
                weaponChosen = 1;
            }
            else if(weaponChosen == 1)
            {
                weaponChosen = 2;
            }
            else if(weaponChosen == 2)
            {
                weaponChosen = 3;
            } 
            else if(weaponChosen == 3)
            {
                weaponChosen = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            if (weaponChosen == 0)
            {
                RangedAttack();
            }
            if (weaponChosen == 1)
            {
                MeleeAttack();
            }
            if (weaponChosen == 2)
            {
                Ranged2Attack();
            }
            if (weaponChosen == 3)
            {
                Melee2Attack();
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Stomp();
        }
    }
    private void RangedAttack()
    {
        if (canFire)
        {
            _bulletsLeft -= 1;
            anim.SetTrigger("Player_Attack");
            Instantiate(bullet, new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z), Quaternion.identity);
            cooldownTimer = 0.7f;
            canFire = false;
        }
        //bullets[0].transform.position = firePoint.position;
        //bullets[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }
    private void Ranged2Attack()
    {
        if (canFire)
        {
            _bulletsLeft -= 1;
            anim.SetTrigger("Player_Shotgun_Attack");
            Instantiate(bigBullet, new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z), Quaternion.identity);
            cooldownTimer = 1f;
            canFire = false;
        }
        //bullets[0].transform.position = firePoint.position;
        //bullets[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }
    private void MeleeAttack()
    {
        if(canMelee)
        {
            anim.SetTrigger("Player_Melee_Attack");
            canMeleeTimer = 0.83f;
            canMelee = false;
        }
    }
    private void Melee2Attack()
    {
        if(canMelee)
        {
            anim.SetTrigger("Player_Melee_Attack_2");
            canMeleeTimer = 0.83f;
            canMelee = false;
        }
    }
    private void Stomp()
    {
        if (canStomp && playerMovement.isGrounded)
        {
            audioManager.PlaySFX(audioManager.stomp); //
            anim.SetTrigger("Player_Stomp");
            cooldownStompTimer = 8f;
            canStomp = false;

            // Kamera sarsıntısını tetikle
            if (cameraShake != null)
            {
                cameraShake.ShakerCamera();
            }
            else
            {
                Debug.LogWarning("CameraShake referansı eksik!");
            }
        }
        else
        {
            Debug.Log(cooldownStompTimer);
        }
    }
}
