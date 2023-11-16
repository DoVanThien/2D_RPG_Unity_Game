using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore;

public class Player : Entity
{
    [Header("Attribute")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveSpeed;

    [Header("Dash")]
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashCoolDown;

    [Header("Attack")]
    [SerializeField] private float _comboTimeCount;
    private float _dashCoolDownTime;
    private float _dashTime;
    private float _xInput;
    private bool _isAttacking;
    private int _comboAttackCount;
    private float _comboTime;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        Movement();
        FlipController();
        AnimotorController();
    }

    void Movement()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
       
        _dashTime -= Time.deltaTime;
        _dashCoolDownTime -= Time.deltaTime;
        _comboTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Dash();
        }

        if (Input.GetKeyDown(KeyCode.J) && _isGround)
        {
            Attack();
        }

        if (_isAttacking) 
        {
            _rb.velocity = new Vector2(0, 0);
        }
        if (_dashTime > 0 && !_isAttacking)
        {
            _rb.velocity = new Vector2(_facingDir * _dashSpeed, 0);
        }
        else if (!_isAttacking)
        {
            _rb.velocity = new Vector2(_xInput * _moveSpeed, _rb.velocity.y);
        }
    }

    void Jump()
    {
        if (_isGround && !_isAttacking)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
    }

    void Dash()
    {
        if (_dashCoolDownTime < 0)
        {
            _dashTime = _dashDuration;
            _dashCoolDownTime = _dashCoolDown;
        }
    }

    void Attack()
    {
        if (_comboTime < 0)
        {
            _comboAttackCount = 0;
        }

        _isAttacking = true;
        _comboTime = _comboTimeCount;
    }

    public void AttackOver()
    {
        _isAttacking = false;
        _comboAttackCount++;

        if (_comboAttackCount > 2)
        {
            _comboAttackCount = 0;
        }
    }

    void AnimotorController()
    {
        bool _isMoving = _xInput != 0;
        _anim.SetBool("IsMoving", _isMoving);
        _anim.SetBool("IsGround", _isGround);
        _anim.SetFloat("yVelocity", _rb.velocity.y);
        _anim.SetBool("IsDashing", _dashTime > 0);
        _anim.SetBool("IsAttacking", _isAttacking);
        _anim.SetInteger("ComboCount", _comboAttackCount);
    }

    void FlipController()
    {
        if (_xInput == -1 && _facingRight && !_isAttacking)
            Flip();
        else if (_xInput == 1 && !_facingRight && !_isAttacking)
            Flip();
    }
}
