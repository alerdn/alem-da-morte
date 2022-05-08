using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        Destroy(GameManager.Instance?.gameObject);
    }
}
