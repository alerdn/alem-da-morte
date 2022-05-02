using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform shootPoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var bullet = BulletManager.Instance.GetBullet();
            if (bullet)
            {
                bullet.SetActive(true);
                bullet.transform.position = shootPoint.position;
                bullet.transform.rotation = shootPoint.rotation;
                bullet.GetComponent<Bullet>()?.StartBullet();
            }
        }
    }
}
