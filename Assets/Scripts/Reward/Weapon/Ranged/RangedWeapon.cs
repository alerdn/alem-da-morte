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

    [Header("Stats")]
    public float shootPerSeconds = 8f;
    public float secondsToReload = 1f;

    private Coroutine _isAttacking = null;
    private Coroutine _isReloading = null;

    private void Start()
    {
        ReloadWeapon();
    }

    public override void Attack()
    {
        if (_isAttacking == null)
            _isAttacking = StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
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

        yield return new WaitForSeconds(1 / shootPerSeconds);
        _isAttacking = null;
    }

    public void ReloadWeapon()
    {
        if (_isReloading == null)
            _isReloading = StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(secondsToReload);

        int ammoToReload = 0;
        if (totalCapacity <= 0 || ammoAmount == cartridgeCapacity)
        {
            Debug.Log("No ammo left or cartridge is full");
            yield break;
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
        _isReloading = null;
    }
}
