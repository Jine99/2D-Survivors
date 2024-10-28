using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
    //특정 메시지함수가 없는 Component는 Enable/Disable이 동작하지 않음.

    //private void Update()
    //{

    //}
    //private void Start()
    //{

    //}
   
    private void Awake()
    {
        //enabled 여부에 관계 없이 호출되는 메시지 함수
    }


    public override void Contact()
    {
        print("폭탄 습득함.");
        GameManager.Instance.RemoveAllEnemys();
        base.Contact();
    }
}
