using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public PartBoss rightArm, body, leftArm;
    public float totalMaxlLife;
    public float totalCurrentLife;

    private void Start()
    {
        totalMaxlLife = rightArm.currentHP + body.currentHP + leftArm.currentHP;
        totalCurrentLife = totalMaxlLife;
    }

    private void Update()
    {
        if (body.gameObject?.activeInHierarchy == false)
        {
            rightArm.Kill();
            leftArm.Kill();
            gameObject.SetActive(false);
        }
        UpdateLife();
    }

    private void UpdateLife()
    {
        totalCurrentLife = rightArm.currentHP + body.currentHP + leftArm.currentHP;
        Debug.Log(totalCurrentLife);
    }
}
