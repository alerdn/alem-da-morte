using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitZone : MonoBehaviour
{
    public GameObject cam2;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        cam2.SetActive(true);
    }
}
