using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance; 
    public float moveSpeed;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection;
    private Vector2 _mousePosition;
    private Animator _animator;
    public WeaponSystem weapons;
    public bool cantMove;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!cantMove)
        {
            _moveDirection.x = Input.GetAxisRaw("Horizontal");
            _moveDirection.y = Input.GetAxisRaw("Vertical");
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _moveDirection.Normalize();

            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!cantMove)
        {
            RotateToMousePosition();
            Move(); 
        }
    }

    public void RotateToMousePosition()
    {
        Vector2 lookToMousePos = _mousePosition - _rigidbody2D.position;
        transform.right = new Vector2(lookToMousePos.x, lookToMousePos.y);
    }

    public void Move()
    { 
        if (_moveDirection != Vector2.zero)
        {
            _animator.SetBool("Move", true);
        }
        else
        {
            _animator.SetBool("Move", false);
        }
        _rigidbody2D.MovePosition(_rigidbody2D.position + _moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void Shoot()
    {
        weapons.usedWeapon.Shoot();
        weapons.UpdateWeaponUI();
    }

    public void Died()
    {
        _animator.SetBool("isDead", true);
        cantMove = true;
    }
}