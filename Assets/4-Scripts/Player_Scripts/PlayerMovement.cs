﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    public float _axisy;
    public float _axisx;
    public float speed = 5.0f;
    public float maxSpeed = 5.0f;
    public float jumpForce = 2.0f;
    public InputActionAsset _asset;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    public bool isGrounded = false; // Karakter başlangıçta yerde

    public static bool canDash = true;
    private float dashingPower = 1f;
    private float dashingTime = 0.75f;
    private float dashingCooldown = 1f;

    public Transform groundCheckTransform;
    private Player_Health playerHealth;

    AudioManager audioManager; //
    private float iFramesDuration;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        playerHealth = GetComponent<Player_Health>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); //
    }

    private IEnumerator Dash()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Invincible");
        audioManager.PlaySFX(audioManager.dash); //
        canDash = false;
        _animator.SetBool("Dash", true);
        float originalGravity = _rigidbody.gravityScale;
        _rigidbody.gravityScale = 0f;
        _rigidbody.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        _rigidbody.gravityScale = originalGravity;
        _animator.SetBool("Dash", false);
        yield return new WaitForSeconds(0.4f); // dash iframe
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

        //iFramesDuration = 0.4f;



    }

    void Update()
    {
        if (playerHealth.dead) return;


        _rigidbody.position += new Vector2(_axisx * speed * Time.deltaTime, 0f);
        //UpdateMovement();
        ClampVelocity();
        CheckGround();

        _animator.SetFloat("Walk", Mathf.Abs(_axisx));
        _animator.SetBool("isJumping", _rigidbody.velocity.y != 0);
        _animator.SetBool("GravityForce", _rigidbody.velocity.y != 0);

        if (_axisx < 0)
        {
            if (isGrounded)
            {
                _animator.SetFloat("Walk", 1);
            }
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (_axisx > 0)
        {
            if (isGrounded)
            {
                _animator.SetFloat("Walk", 1);
            }
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        float moveInput = Input.GetAxis("Horizontal");

        // Eğer karakter yere temas ediyorsa yürüyebilir
        if (isGrounded)
        {
            transform.position += new Vector3(moveInput * speed * Time.deltaTime, 0, 0);
        }

        // Yön değişikliği her zaman aktif olmalı
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Zıplama Kontrolü

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    //private void UpdateMovement()
    //{

    //    Vector2 movement = Vector2.zero;
    //    if (Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        movement.x = -1;
    //    }
    //    if (Input.GetKey(KeyCode.RightArrow))
    //    {
    //        movement.x = 1;
    //    }
    //    if (movement == Vector2.zero)
    //    {
    //        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    //    }

    //    _rigidbody.AddForce(movement * speed);
    //}

    //private void UpdateJump()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    //    {
    //        _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    //    }
    //}
    private void ClampVelocity()
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        _rigidbody.velocity = velocity;
    }

    private void OnEnable()
    {
        _asset.Enable();
        _asset.FindAction("Player/Move").started += HandleMove;
        _asset.FindAction("Player/Move").performed += HandleMove;
        _asset.FindAction("Player/Move").canceled += HandleMoveStop;

        _asset.FindAction("Player/Jump").started += HandleJump;
        _asset.FindAction("Player/Jump").performed += HandleJump;
        //_asset.FindAction("Player/Jump").canceled += HandleJumpDown;

    }
    private void OnDisable()
    {
        _asset.Disable();
        _asset.FindAction("Player/Move").started -= HandleMove;
        _asset.FindAction("Player/Move").performed -= HandleMove;
        _asset.FindAction("Player/Move").canceled -= HandleMoveStop;

        _asset.FindAction("player/jump").started -= HandleJump;
        _asset.FindAction("Player/Jump").performed -= HandleJump;
        _asset.FindAction("Player/Jump").performed -= HandleJump;
        //_asset.FindAction("Player/Jump").canceled -= HandleJumpDown;

    }
    void HandleMove(InputAction.CallbackContext ctx)
    {
        _axisx = ctx.ReadValue<float>();
    }
    void HandleMoveStop(InputAction.CallbackContext ctx)
    {
        _axisx = ctx.ReadValue<float>();
        //_animator.Play("character_animation_idle");
    }

    public void HandleJump(InputAction.CallbackContext ctx)
    {
        if (isGrounded && !playerHealth.dead)
        {
            _rigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            audioManager.PlaySFX(audioManager.jump); //
        }
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheckTransform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
                _animator.SetBool("Grounded", true);
            }

            //else
            //{
            //    isGrounded = false;
            //    _animator.SetBool("Grounded", false);
            //}
        }
        else
        {
            isGrounded = false;
            _animator.SetBool("Grounded", false);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //        _animator.SetBool("Grounded", true);
    //    }
    //    // Yere temas kontrolü (örneğin, bir Collider ile)
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = false;
    //        _animator.SetBool("Grounded", false);
    //    }

    //}

}

