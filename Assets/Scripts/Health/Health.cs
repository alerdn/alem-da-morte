using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [Header("Health setup")]
    public float maxHP;

    [Header("Debug only")]
    public float currentHP;

    private void Awake() {
        currentHP = maxHP;
    }

    public abstract void Damage(float d);
    public abstract void Kill();
}
