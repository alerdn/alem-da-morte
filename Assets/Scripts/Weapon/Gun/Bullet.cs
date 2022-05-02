using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 1f;
    public float timeToReset = 5f;
    public Rigidbody2D rb;

    void Update()
    {
        // ver isso aqui Debug.Log(transform.rotation.z);
        float rad = transform.rotation.z * Mathf.Deg2Rad;
        Vector2 _aim = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        rb.velocity = _aim.normalized * speed;
    }

    public void StartBullet()
    {
        Invoke(nameof(FinishUsage), timeToReset);
    }

    private void FinishUsage()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<IDamageable>()?.Damage(damage);
        gameObject.SetActive(false);
    }
}
