using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public Blade blade;

    public override void Attack()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Attack");
    }
}
