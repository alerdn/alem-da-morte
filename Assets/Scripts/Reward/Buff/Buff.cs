using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : Reward
{
    public virtual void ApplyBuff()
    {
        Player p = GameManager.Instance.player;
        p.buffs.Add(this);
    }

    public virtual void UpdateBuff()
    {
        return;
    }
}
