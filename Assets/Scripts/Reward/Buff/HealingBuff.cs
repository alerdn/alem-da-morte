using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBuff : Buff
{
    [Header("Heal setup")]
    public float healPerSeconds = 0.001f;

    private Player _player;

    public override void ApplyBuff()
    {
        base.ApplyBuff();

        _player = GameManager.Instance.player;
        InvokeRepeating(nameof(Heal), 0f, 1f);
    }
    
    private void Heal()
    {
        if (_player.currentHP == _player.maxHP) return;
        _player.Heal(healPerSeconds);
    }
}
