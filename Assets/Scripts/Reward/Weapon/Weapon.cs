using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Reward
{
    public int damage = 1;

    public abstract void Attack();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            GameManager.Instance.player.EquipWeapon(this);
        }
    }
}
