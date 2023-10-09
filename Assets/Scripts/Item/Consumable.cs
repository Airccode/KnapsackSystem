using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 消耗品类
/// </summary>
public class Consumable : Item
{

    public int HP { get; set; }
    public int MP { get; set; }

    public Consumable(int id, string name, ItemType type, ItemQuality quality, string des, int capacity, int buyPrice, int sellPrice, string sprite
        ,int hp,int mp)
        : base(id, name, type, quality, des, capacity, buyPrice, sellPrice, sprite)
    {
        HP = hp;
        MP = mp;
    }
    public override string GetItemDescription()
    {
        string text = base.GetItemDescription();
        string newText = string.Format("{0}\n<size=12><color=red>加HP:{1}</color>\n<color=blue>加MP:{2}</color></size>\n", text, HP, MP);
        return newText;
    }
}
