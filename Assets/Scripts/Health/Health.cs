using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [Header("Health setup")]
    public int initialHP;

    protected int _currentHP;

    public abstract void Damage(int d);
    public abstract void Kill();
}
