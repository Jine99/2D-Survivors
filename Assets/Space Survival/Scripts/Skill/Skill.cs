using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // �˷��ּ����� ���� �����... �˾ƺ���(MonoBehaviour ��� �޴´�� �����Ϸ��� ����Ѵٴ���)
public class Skill
{
    public string skillName;
    public int skillLevel;
    //public GameObject skillPrefab;
    public bool isTargeting; // ���� ����� ���� ��ų����(��������)

    public GameObject[] skillperfabs;
    public GameObject currentSkillobject;


}
