using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Singleton Missing");
            }
            return _instance;
        }
    }
    public virtual void Awake()
    {
        if(_instance == null)
            _instance = (T)this;
        else
        {
            Debug.LogWarning("Multiple Instanses of MonoSingleton");
            Destroy(this.gameObject);
        }
    }

}