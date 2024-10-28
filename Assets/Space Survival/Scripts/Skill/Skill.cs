using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // 알려주셨지만 나는 까먹음... 알아보자(MonoBehaviour 상속 받는대신 적용하려면 써야한다던데)
public class Skill
{
    public string skillName;
    public int skillLevel;
    //public GameObject skillPrefab;
    public bool isTargeting; // 가장 가까운 적이 스킬인지(누구인지)

    public GameObject[] skillperfabs;
    public GameObject currentSkillobject;


}
