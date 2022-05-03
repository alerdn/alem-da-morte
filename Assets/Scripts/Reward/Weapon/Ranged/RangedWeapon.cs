using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [Header("Ranged")]
    public Transform shootPoint;
    public int totalCapacity = 30;
    public int cartridgeCapacity = 10;
    public int ammoAmount = 0;

    private void Start() {
        ReloadWeapon();
    }

    public override void Attack()
    {
        if (ammoAmount > 0)
        {
            var bullet = PlayerBulletManager.Instance.GetBullet();
            if (bullet)
            {
                bullet.SetActive(true);
                bullet.transform.position = shootPoint.position;
                bullet.transform.rotation = shootPoint.rotation;
                bullet.GetComponent<Bullet>()?.StartBullet(damage);

                ammoAmount--;
            }
        }
        else Debug.Log("Need to reload the gun!");
    }

    public void ReloadWeapon()
    {
        int ammoToReload = 0;
        if (totalCapacity <= 0 || ammoAmount == cartridgeCapacity)
        {
            Debug.Log("No ammo left or cartridge is full");
            return;
        }

        /* Se tem municação de sobra, recarrega só o que cabe no cartucho*/
        if (totalCapacity > cartridgeCapacity)
        {
            ammoToReload = cartridgeCapacity;
        }
        /* Se não recarrega tudo o que sobrou */
        else
        {
            ammoToReload = totalCapacity;
        }

        /* Recarrega até o limite */
        if (ammoToReload + ammoAmount > cartridgeCapacity)
        {
            ammoToReload = cartridgeCapacity - ammoAmount;
        }

        /* Remove a quantidade recarregada do total */
        totalCapacity -= ammoToReload;

        /* Recarrega a arma */
        ammoAmount += ammoToReload;

        Debug.Log("Weapon realoded!");
    }
}
