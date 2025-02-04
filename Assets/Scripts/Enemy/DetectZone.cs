using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZone : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag != "Player") return;

        enemy.FollowPlayer();
    }
}
