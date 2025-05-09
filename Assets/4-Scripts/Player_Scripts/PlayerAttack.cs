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
    //public Transform _canvas;
    //public Transform _slider;

    private void Awake()
    {
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
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Attack();
        }
    }
    private void Attack()
    {
        if (canFire)
        {
            _bulletsLeft -= 1;
            anim.SetTrigger("attack");
            Instantiate(bullet, new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z), Quaternion.identity);
            cooldownTimer = 0.7f;
            canFire = false;
        }
        //bullets[0].transform.position = firePoint.position;
        //bullets[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }


}
