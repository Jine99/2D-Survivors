using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
    //Ư�� �޽����Լ��� ���� Component�� Enable/Disable�� �������� ����.

    //private void Update()
    //{

    //}
    //private void Start()
    //{

    //}
   
    private void Awake()
    {
        //enabled ���ο� ���� ���� ȣ��Ǵ� �޽��� �Լ�
    }


    public override void Contact()
    {
        print("��ź ������.");
        GameManager.Instance.RemoveAllEnemys();
        base.Contact();
    }
}
