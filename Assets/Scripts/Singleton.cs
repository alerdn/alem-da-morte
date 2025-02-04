using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Singleton instance is null");

            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = GetComponent<T>();
        }
        else Destroy(gameObject);
    }
}
