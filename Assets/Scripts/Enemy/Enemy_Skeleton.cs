using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy_Skeleton : Entity
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _playerCheckDistance;
    [SerializeField] private float _attackDistance;
    private RaycastHit2D _detectionPlayer;
    private bool _isDetectionPlayer;
    private bool _isAttacking = false;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        Debug.Log(_isDetectionPlayer);
        if (_isDetectionPlayer)
        {
            if(_detectionPlayer.distance < _attackDistance)
            {
                _isAttacking = true;
                Attack();
            }
            else
            {
                _rb.velocity = new Vector2(_facingDir * _speed * 2f, transform.position.y);
                _isAttacking = false;
            }
        }
        else
        {
            _isAttacking = false;
            Movement();
        }

        if(!_isGround || _isWall)
        {
            Flip();
        }
    }

    void Attack()
    {
        Debug.Log("Attack " + _isAttacking);
    }

    void Movement()
    {
        if (!_isAttacking)
        {
            _rb.velocity = new Vector2(_facingDir * _speed, transform.position.y);
        }
    }

    protected override void CollisionCheck()
    {
        base.CollisionCheck();
        _detectionPlayer = Physics2D.Raycast(transform.position, Vector2.right, _playerCheckDistance * _facingDir, _playerLayer);
        _isDetectionPlayer = _detectionPlayer;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + _playerCheckDistance * _facingDir, transform.position.y));
    }
}
