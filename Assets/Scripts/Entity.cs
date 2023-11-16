using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected bool _isGround;
    [SerializeField] protected bool _isWall;
    [SerializeField] protected LayerMask _ground;
    [SerializeField] protected float _groundCheckDistance;
    [SerializeField] protected Transform _groundCheck;
    [SerializeField] protected float _wallCheckDistance;
    [SerializeField] protected Transform _wallCheck;
    protected bool _facingRight = true;
    protected int _facingDir = 1;


    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();

        if(_wallCheck == null)
        {
            _wallCheck = transform;
        }
    }

    protected virtual void Update()
    {
        CollisionCheck();
    }

    protected virtual void CollisionCheck()
    {
        _isGround = Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _ground);
        _isWall = Physics2D.Raycast(_wallCheck.position, Vector2.right, _wallCheckDistance * _facingDir, _ground);
    }

    protected virtual void Flip()
    {
        _facingRight = !_facingRight;
        _facingDir *= -1;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void OnDrawGizmos() {
        Gizmos.DrawLine(_groundCheck.position, new Vector3(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance));
        Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + _wallCheckDistance  * _facingDir, _wallCheck.position.y));
    }
}
