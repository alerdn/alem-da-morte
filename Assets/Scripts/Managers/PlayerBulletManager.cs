using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public List<GameObject> bulletPool;
    public int amount = 20;

    #region Singleton
    private static PlayerBulletManager _instance;
    public static PlayerBulletManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("UIManager instance is null");

            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = GetComponent<PlayerBulletManager>();
        }
        else Destroy(gameObject);
    }
    #endregion

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
