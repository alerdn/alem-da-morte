using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health
{
    [Header("Enemy setup")]
    public float damage = 1f;
    public float attackSpeed = 1f;
    public float moveSpeed = 1f;
    public Collider2D enemySpace;
    public Animator anim;
    public SpriteRenderer render;

    [Range(0, 1)]
    public float dropRatio = 0.3f;

    [Header("Enemy sight")]
    public float sightRange;
    public LayerMask playerLayer;

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

    private void Update()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, sightRange, playerLayer);
        if (playerCollider) FollowPlayer();
    }

    private void OnTriggerStay2D(Collider2D collision)
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
        anim.SetTrigger("SawPlayer");
    }

    public override void Damage(float d)
    {
        HitColor();
        FollowPlayer();

        currentHP -= d;
        if (currentHP <= 0) Kill();
    }

    public override void Kill()
    {
        // Drop collectible
        if (Random.Range(0f, 1f) <= dropRatio)
        {
            var col = Instantiate(GameManager.Instance.GetRandomDrop());
            col.transform.position = transform.position;
        }

        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void HitColor()
    {
        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        render.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        render.color = Color.white;
    }
}
