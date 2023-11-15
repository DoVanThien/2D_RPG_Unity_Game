using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class Player : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _anim;
    [SerializeField] private LayerMask ground;

    [Header("Attribute")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveSpeed;
    
    [Header("Dash")]
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashCoolDown;
    private float _dashCoolDownTime;
    private float _dashTime;
    private float _groundCheckDistance = 1.5f;
    private bool _isGround;
    private float _xInput;
    private bool _facingRight = true;

    void Start()
    {

    }

    void Update()
    {
        Movement();
        FlipController();
        AnimotorController();
    }

    void Movement()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        _isGround = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, ground);
        _dashTime -= Time.deltaTime;
        _dashCoolDownTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Dash();
        }

        if (_dashTime > 0)
        {
            _rb.velocity = new Vector2(_xInput * _dashSpeed, 0);
        }
        else
        {
            _rb.velocity = new Vector2(_xInput * _moveSpeed, _rb.velocity.y);
        }
    }

    void Jump()
    {
        if (_isGround)
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

    void AnimotorController()
    {
        bool _isMoving = _xInput != 0;
        _anim.SetBool("IsMoving", _isMoving);
        _anim.SetBool("IsGround", _isGround);
        _anim.SetFloat("yVelocity", _rb.velocity.y);
        _anim.SetBool("IsDashing", _dashTime > 0);
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }

    void FlipController()
    {
        if (_xInput == -1 && _facingRight)
            Flip();
        else if (_xInput == 1 && !_facingRight)
            Flip();
    }
}
