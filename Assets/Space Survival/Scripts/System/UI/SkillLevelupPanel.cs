using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillLevelupPanel : MonoBehaviour
{
    public RectTransform list;
    public SKillLevelUpButton buttonPrefab;

    //플레이어가 레벨업을 하면 패널 활성화 요청.
    public void LevelUpPanelOpen(List<Skill> skillList, Action<Skill> callback)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        //스킬 2개 UI에 표시할 예정
        List<Skill> selectedSkillList = new();


        while (selectedSkillList.Count < 2)
        {
            //2개의 스킬이 선택 될때까지 반복
            int ranNum =  UnityEngine.Random.Range(0,skillList.Count);

            Skill selectedSkill = skillList[ranNum];//랜덤하게 선택된 스킬가져오기.

            if (selectedSkillList.Contains(selectedSkill)) 
            {
                //이미 뽑힌 스킬이 또 뽑혔으면 컨티뉴로 다시반복
                continue;
            }
            //뽑힌 스킬을 List에 추가한다.
            selectedSkillList.Add(selectedSkill);

            //선택된 스킬을 레벨업 하는 버튼을 생성
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
            //생성된 리스트 제거
            Destroy(buttons.gameObject);
        }
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

}
