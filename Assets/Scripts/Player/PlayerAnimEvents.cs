using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    private Player _player;
    void Start()
    {
        _player = GetComponentInParent<Player>();
        if (_player == null)
        {
            Debug.Log("Player is null");
        }
    }

    void AttackOverEvent()
    {
        _player.AttackOver();
    }
}
