using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [Header("Health setup")]
    public int initialHP;
    [SerializeField]
    protected int _currentHP;

    private void Awake() {
        _currentHP = initialHP;
    }

    public abstract void Damage(int d);
    public abstract void Kill();
}
