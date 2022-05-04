using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health
{
    [Header("Enemy setup")]
    public int damage = 1;
    public float attackSpeed = 1f;
    public float moveSpeed = 1f;

    private SeekerAI _seeker;
    private Coroutine _isAttacking = null;

    private void Start()
    {
        _seeker = GetComponent<SeekerAI>();
        _seeker.target = GameManager.Instance.player.transform;
        _seeker.speed *= moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player") return;

        _seeker.isSeeking = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
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
