using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//�÷��̾� �����͸� ����ϴ� ScriptableObject
//2. ���� ���� �޴��� ���
[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/Player Dara" ,order = 0)]
public class PlayerDataSO : ScriptableObject //1. MonoBehaviour ���  ScriptableObject�� ���
{
    //asset ���Ϸ� ���� �� �����͸� �Է��� �� ����.
    public string characterName;
    public float hp;
    public float damage;
    public float moveSpeed;

    public Sprite sprite;
    public GameObject startSkillPrefab;
    public bool rotatRenderer;


}
