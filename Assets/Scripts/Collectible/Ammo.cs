using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Collectible
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player") return;

        int ammoAmount = Random.Range(10, 60);
        Weapon w = GameManager.Instance.player.weapon;

        w.RefillCapacity(ammoAmount);
        Debug.Log($"Collected {ammoAmount} bullets");

        Destroy(gameObject);
    }
}
