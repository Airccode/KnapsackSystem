using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    /// <summary>
    /// 力量、智力、敏捷、体力
    /// </summary>
    public int Strength { get; set; }
    public int Intellect { get; set; }
    public int Agility { get; set; }
    public int Stamina { get; set; }

    public EquipmentType EquipType { get; set; }

    public Equipment(int id, string name, ItemType type, ItemQuality quality, string des, int capacity, int buyPrice, int sellPrice,string sprite
        ,int strength, int intellect, int agility, int stamina,EquipmentType equipmentType)
        : base(id, name, type, quality, des, capacity, buyPrice, sellPrice,sprite)
    {
        Strength = strength;
        Intellect = intellect;
        Agility = agility;
        Stamina = stamina;
        EquipType = equipmentType;
    }

    /// <summary>
    /// 装备类型
    /// </summary>
    public enum EquipmentType
    {
        Head,
        Chest,
        Neck,
        Ring,
        Leg,
        Bracer,
        Trinket,
        Shoulder,
        Belt,
        OffHand,
        Boots
    }
    public override string GetItemDescription()
    {
        string text = base.GetItemDescription();
        string equipType = "";
        switch (EquipType)
        {
            case EquipmentType.Head:
                equipType = "头部";
                break;
            case EquipmentType.Chest:
                equipType = "胸部";
                break;
            case EquipmentType.Neck:
                equipType = "脖子";
                break;
            case EquipmentType.Ring:
                equipType = "戒指";
                break;
            case EquipmentType.Leg:
                equipType = "护腿";
                break;
            case EquipmentType.Bracer:
                equipType = "护腕";
                break;
            case EquipmentType.Trinket:
                equipType = "饰品";
                break;
            case EquipmentType.Shoulder:
                equipType = "肩膀";
                break;
            case EquipmentType.Belt:
                equipType = "腰带";
                break;
            case EquipmentType.OffHand:
                equipType = "副手";
                break;
            case EquipmentType.Boots:
                equipType = "鞋子";
                break;
        }
        string newText = string.Format("{0}\n<size=12>装备类型:{5}</size>\n<color=blue>智力:{1}\n力量:{2}\n敏捷:{3}\n体力:{4}</color>\n", text,Intellect,Strength,Agility,Stamina,equipType);
        return newText;
    }
}
