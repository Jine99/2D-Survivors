using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IContactable
{

    //가상함수로 오버라이드 가능함
    public virtual void Contact()
    {

        Destroy(gameObject);
    }

}
