using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Collectible
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player") return;
        
        Weapon w = GameManager.Instance.player._currentWeapon;
        if (w.totalCapacity == w.maxCapacity) return;

        int ammoAmount = Random.Range(10, 60);

        w.RefillCapacity(ammoAmount);
        Debug.Log($"Collected {ammoAmount} bullets");

        Destroy(gameObject);
    }
}
