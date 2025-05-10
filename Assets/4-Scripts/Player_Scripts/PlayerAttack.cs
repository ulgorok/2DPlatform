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
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = 0f;
    public static bool canFire;
    public static int _bulletsLeft;
    public int weaponChosen;
    public bool canMelee;
    public float canMeleeTimer;
    //public Transform _canvas;
    //public Transform _slider;

    private void Awake()
    {
        canMelee = true;
        weaponChosen = 0;
        _bulletsLeft = 5;
        canFire = true;
        //_canvas = this.gameObject.transform.Find("ReloadCanvas");
        //_slider = _canvas.transform.Find("ReloadSlider");
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        firePoint.position = this.gameObject.transform.Find("FirePoint").position;
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
    private void MeleeAttack()
    {
        if(canMelee)
        {
            anim.SetTrigger("Player_Melee_Attack");
            canMeleeTimer = 0.83f;
            canMelee = false;
        }
    }


}
