using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SKillLevelUpButton : MonoBehaviour
{
    public Text SkillNameText;
    public Button button;

    public void SetSkillSelectButton(string SkillName, UnityAction onClick)
    {
        SkillNameText.text = SkillName;
        button.onClick.AddListener(()=>onClick());
    }

}
