using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damage = 1;

    public abstract void Attack();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            GameManager.Instance.player.EquipWeapon(this);
        }
    }
}
