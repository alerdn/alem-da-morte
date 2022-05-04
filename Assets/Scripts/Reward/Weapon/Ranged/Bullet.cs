using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 1f;
    public float timeToReset = 5f;

    void FixedUpdate()
    {
        Shoot();
    }

    private void Shoot()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    public void StartBullet(int damage)
    {
        this.damage = damage;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        Invoke(nameof(FinishUsage), timeToReset);
    }

    private void FinishUsage()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetType() == this.GetType() || collision.transform.tag == "FloorLimit")
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            return;
        }

        collision.gameObject.GetComponent<IDamageable>()?.Damage(damage);
        gameObject.SetActive(false);
    }
}
