using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Item
{
    public float healAmount; // Èú·®
    public override void Contact()
    {
        print("È¸º¹¾à ½ÀµæÇÔ");

        GameManager.Instance.player.TakeHeal(healAmount);
        base.Contact();
    }
}
