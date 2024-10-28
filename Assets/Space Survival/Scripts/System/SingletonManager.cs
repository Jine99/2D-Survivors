using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SingletonManager<T> : MonoBehaviour where T : MonoBehaviour
{                     //부모인 싱글턴 매니저는 모노 상속함 캐스팅 때문에 제약조건을 설정해야함
    private static T instance;
    public static T Instance { get { return instance; } } // Get만 가능한 public propertie.

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
}
