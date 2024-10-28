using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillLevelupPanel : MonoBehaviour
{
    public RectTransform list;
    public SKillLevelUpButton buttonPrefab;

    //�÷��̾ �������� �ϸ� �г� Ȱ��ȭ ��û.
    public void LevelUpPanelOpen(List<Skill> skillList, Action<Skill> callback)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        //��ų 2�� UI�� ǥ���� ����
        List<Skill> selectedSkillList = new();


        while (selectedSkillList.Count < 2)
        {
            //2���� ��ų�� ���� �ɶ����� �ݺ�
            int ranNum =  UnityEngine.Random.Range(0,skillList.Count);

            Skill selectedSkill = skillList[ranNum];//�����ϰ� ���õ� ��ų��������.

            if (selectedSkillList.Contains(selectedSkill)) 
            {
                //�̹� ���� ��ų�� �� �������� ��Ƽ���� �ٽùݺ�
                continue;
            }
            //���� ��ų�� List�� �߰��Ѵ�.
            selectedSkillList.Add(selectedSkill);

            //���õ� ��ų�� ������ �ϴ� ��ư�� ����
            SKillLevelUpButton skillbutoon = Instantiate(buttonPrefab, list);

            skillbutoon.SetSkillSelectButton(selectedSkill.skillName,
                ()=> {
                    callback(selectedSkill);
                    LevelUpPanelClose(); });
        } 

    }
    public void LevelUpPanelClose()
    {
        foreach(Transform buttons in list)
        {
            //������ ����Ʈ ����
            Destroy(buttons.gameObject);
        }
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

}
