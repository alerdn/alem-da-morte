using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{
    public GameObject bulletPrefab;
    public List<GameObject> bulletPool;
    public int amount = 20;

    private void Start()
    {
        bulletPool = new List<GameObject>();

        for (int i = 0; i < amount; i++)
        {
            var bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (var bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
                return bullet;
        }

        return null;
    }
}
