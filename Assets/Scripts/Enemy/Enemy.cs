using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health
{
    [Header("Enemy setup")]
    public int damage;
    public float attackSpeed = 1f;
    public float moveSpeed = 1f;

    private SeekerAI _seeker;

    private void Start()
    {
        _seeker = GetComponent<SeekerAI>();
        _seeker.target = GameManager.Instance.player.transform;
        _seeker.speed *= moveSpeed;
    }

    public override void Damage(int d)
    {
        _currentHP -= d;
        if (_currentHP <= 0) Kill();
    }

    public override void Kill()
    {
        Destroy(gameObject);
    }
}
