using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public Transform shootPoint;

    public override void Attack()
    {
        var bullet = PlayerBulletManager.Instance.GetBullet();
        if (bullet)
        {
            bullet.SetActive(true);
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = shootPoint.rotation;
            bullet.GetComponent<Bullet>()?.StartBullet(damage);
        }
    }
}
