using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 物品类
/// </summary>
public class Item
{

    public int ID { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public ItemQuality Quality { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public string Sprite { get; set; }

    public Item(int id,string name,ItemType type,ItemQuality quality,string des,int capacity,int buyPrice,int sellPrice,string sprite)
    {
        ID = id;
        Name = name;
        Type = type;
        Quality = quality;
        Description = des;
        Capacity = capacity;
        BuyPrice = buyPrice;
        SellPrice = sellPrice;
        Sprite = sprite;
    }
    public Item()
    {
        ID = -1;
    }
    /// <summary>
    /// 物品类型
    /// </summary>
    public enum ItemType
    {
        Consumable,
        Equipment,
        Weapon,
        Material
    }
    /// <summary>
    /// 品质类型
    /// </summary>
    public enum ItemQuality
    {
        Common,
        UnCommon,
        Rare,
        Epic,
        Legendary,
        Artifact
    }

    public virtual string GetItemDescription()
    {
        StringBuilder text = new StringBuilder();
        string color = "";
        switch (Quality)
        {
            case ItemQuality.Common:
                color = "white";
                break;
            case ItemQuality.UnCommon:
                color = "lime";
                break;
            case ItemQuality.Rare:
                color = "navy";
                break;
            case ItemQuality.Epic:
                color = "magenta";
                break;
            case ItemQuality.Legendary:
                color = "orange";
                break;
            case ItemQuality.Artifact:
                color = "red";
                break;
        }
        text.AppendFormat("<color={0}>{1}</color>\n<color=black><size=12>{2}</size></color>\n" +
            "<color=yellow><size=10>购买价格:{3}\n出售价格:{4}</size></color>\n",
            color, Name,Description,BuyPrice,SellPrice);
        return text.ToString();
        //TODO
    }

}
