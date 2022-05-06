using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Reward
{
    [Header("Weapon setup")]
    public bool simpleWeapon = false;
    public float damage = 1f;
    public Transform shootPoint;
    public int maxCapacity = 30;
    public int cartridgeCapacity = 10;
    public float shootPerSeconds = 8f;
    public float secondsToReload = 1f;

    [Header("Debug only")]
    public int totalCapacity = 0;
    public int ammoAmount = 0;
    public float damageMultiplier = 1f;

    private Coroutine _isAttacking = null;
    private Coroutine _isReloading = null;

    private void Start()
    {
        totalCapacity = maxCapacity;
        ReloadWeapon();
    }

    public void Attack()
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
                float bulletDamage = damage * damageMultiplier;

                bullet.SetActive(true);
                bullet.transform.position = shootPoint.position;
                bullet.transform.rotation = shootPoint.rotation;
                bullet.GetComponent<Bullet>()?.StartBullet(bulletDamage);

                ammoAmount--;
            }
        }
        else ReloadWeapon();

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
            _isReloading = null;
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
        if (!simpleWeapon)totalCapacity -= ammoToReload;

        /* Recarrega a arma */
        ammoAmount += ammoToReload;

        _isReloading = null;
    }

    public void RefillCapacity(int c)
    {
        int limitToRefil = maxCapacity - totalCapacity;

        if (c > limitToRefil)
        {
            totalCapacity += limitToRefil;
        }
        else
        {
            totalCapacity += c;
        }
    }
}
