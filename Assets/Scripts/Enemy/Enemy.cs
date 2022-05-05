using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health
{
    [Header("Enemy setup")]
    public int damage = 1;
    public float attackSpeed = 1f;
    public float moveSpeed = 1f;
    public Collider2D enemySpace;

    private SeekerAI _seeker;
    private Coroutine _isAttacking = null;

    private void Start()
    {
        var p = GameManager.Instance.player;

        _seeker = GetComponent<SeekerAI>();
        _seeker.target = p.transform;
        _seeker.speed *= moveSpeed;

        Physics2D.IgnoreCollision(p.GetComponent<Collider2D>(), enemySpace);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player") return;

        if (_isAttacking == null)
            _isAttacking = StartCoroutine(Attack(GameManager.Instance.player));
    }

    IEnumerator Attack(Player p)
    {
        p.Damage(damage);

        yield return new WaitForSeconds(1 / attackSpeed);
        _isAttacking = null;
    }

    public void FollowPlayer()
    {
        _seeker.isSeeking = true;
    }

    public override void Damage(int d)
    {
        currentHP -= d;
        if (currentHP <= 0) Kill();
    }

    public override void Kill()
    {
        Destroy(gameObject);
    }
}
