using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [Header("Health setup")]
    public int initialHP;

    [Header("Debug only")]
    public int currentHP;

    private void Awake() {
        currentHP = initialHP;
    }

    public abstract void Damage(int d);
    public abstract void Kill();
}
