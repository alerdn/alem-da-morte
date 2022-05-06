using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : Collectible
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player") return;
        
        Player p = GameManager.Instance.player;
        if (p.currentHP == p.maxHP) return;

        float hpToHeal = Random.Range(1f, 3f);
        p.Heal(hpToHeal);

        Debug.Log($"Player healed with {hpToHeal} HP");
        Destroy(gameObject);
    }
}
