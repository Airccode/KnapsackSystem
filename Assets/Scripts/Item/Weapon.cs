using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int Damage { get; set; }

    public WeaponType WPType { get; set; }

    public Weapon(int id, string name, ItemType type, ItemQuality quality, string des, int capacity, int buyPrice, int sellPrice,string sprite
        ,int damage,WeaponType wpType)
        : base(id, name, type, quality, des, capacity, buyPrice, sellPrice,sprite)
    {
        Damage = damage;
        WPType = wpType;
    }

    public enum WeaponType
    {
        OffHand,
        MainHand
    }
    public override string GetItemDescription()
    {
        string text = base.GetItemDescription();
        string wpType = "";
        switch (WPType)
        {
            case WeaponType.OffHand:
                wpType = "副武器";
                break;
            case WeaponType.MainHand:
                wpType = "主武器";
                break;
        }
        string newText = string.Format("{0}\n<size=12>武器类型:{1}</size>\n<color=blue>攻击力:{2}</color>\n", text,wpType,Damage);
        return newText;
    }
}
