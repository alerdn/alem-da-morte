using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1f;
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

    public void StartBullet(float damage)
    {
        this.damage = damage;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        Invoke(nameof(FinishUsage), timeToReset);
    }

    private void FinishUsage()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == "Obstacle" || collider.transform.tag == "Enemy")
        {
            Debug.Log("Destruiu bullet");
            collider.gameObject.GetComponent<Health>()?.Damage(damage);
            gameObject.SetActive(false);
        }
    }
}
