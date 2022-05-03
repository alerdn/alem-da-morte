using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (!GameManager.Instance.player.isAttacking) return;

        Debug.Log("Acertou");
    }
}
