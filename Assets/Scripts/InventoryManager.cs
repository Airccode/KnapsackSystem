using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using System.Reflection;

public class InventoryManager : MonoBehaviour
{
    #region 单例模式
    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return _instance;
        }
    }
    #endregion
    private List<Item> itemList;
    private ToolTip toolTip;

    public void Start()
    {
        itemList = new List<Item>();
        toolTip = GameObject.FindAnyObjectByType<ToolTip>();
        //toolTip.Hide();
        ParseItemJson();
    }
    /// <summary>
    /// 解析物品信息
    /// </summary>
    void ParseItemJson()
    {
        itemList = new List<Item>();
        TextAsset itemText = Resources.Load<TextAsset>("content");
        string itemJson = itemText.text;
        //Debug.Log(itemJson);
        JsonReader reader = new JsonReader(itemJson);
        //while (!reader.EndOfJson)
        //{
        //    reader.Read();
        //    Debug.Log(reader.Value);
        //}

        /*
            public int ID { get; set; }
            public string Name { get; set; }
            public ItemType Type { get; set; }
            public ItemQuality Quality { get; set; }
            public string Description { get; set; }
            public int Capacity { get; set; }
            public int BuyPrice { get; set; }
            public int SellPrice { get; set; }
            public string Sprite { get; set; }
         */
        JsonData data = JsonMapper.ToObject(itemJson);
        foreach(JsonData elem in data)
        {
            int ID = int.Parse(elem["ID"].ToString());
            string Name = elem["Name"].ToString();
            Item.ItemType Type = (Item.ItemType) Enum.Parse(typeof(Item.ItemType), elem["Type"].ToString());
            Item.ItemQuality Quality = (Item.ItemQuality)Enum.Parse(typeof(Item.ItemQuality), elem["Quality"].ToString());
            string Description = elem["Description"].ToString();
            int Capacity = int.Parse(elem["Capacity"].ToString());
            int BuyPrice = int.Parse(elem["BuyPrice"].ToString());
            int SellPrice = int.Parse(elem["SellPrice"].ToString());
            string Sprite = elem["Sprite"].ToString();
            Item item = null;
            switch (Type)
            {
                /*
                 * Consumable Properties
                    public int HP { get; set; }
                    public int MP { get; set; }
                 */
                case Item.ItemType.Consumable:
                    int HP = int.Parse(elem["HP"].ToString());
                    int MP = int.Parse(elem["MP"].ToString());
                    item = new Consumable(ID, Name, Type, Quality, Description, Capacity, BuyPrice, SellPrice, Sprite
                        , HP, MP);
                    break;

                /*
                 * Equipment Properties
                    public int Strength { get; set; }
                    public int Intellect { get; set; }
                    public int Agility { get; set; }
                    public int Stamina { get; set; }
                 */
                case Item.ItemType.Equipment:
                    int Strength = int.Parse(elem["Strength"].ToString());
                    int Intellect = int.Parse(elem["Intellect"].ToString());
                    int Agility = int.Parse(elem["Agility"].ToString());
                    int Stamina = int.Parse(elem["Stamina"].ToString());
                    Equipment.EquipmentType equipmentType = (Equipment.EquipmentType)Enum.Parse(typeof(Equipment.EquipmentType), elem["EquipType"].ToString());
                    item = new Equipment(ID, Name, Type, Quality, Description, Capacity, BuyPrice, SellPrice, Sprite
                        ,Strength,Intellect,Agility,Stamina,equipmentType);
                    break;
                /*
                 * Material Properties
                    TODO
                 */
                case Item.ItemType.Material:
                    item = new Material(ID, Name, Type, Quality, Description, Capacity, BuyPrice, SellPrice, Sprite);
                    break;
                /*
                 * Weapon Properties
                    public int Damage { get; set; }
                    public WeaponType WPType { get; set; }
                 */
                case Item.ItemType.Weapon:
                    int Damage = int.Parse(elem["Damage"].ToString());
                    Weapon.WeaponType WPType = (Weapon.WeaponType)Enum.Parse(typeof(Weapon.WeaponType), elem["WPType"].ToString());
                    item = new Weapon(ID, Name, Type, Quality, Description, Capacity, BuyPrice, SellPrice, Sprite
                        ,Damage,WPType);
                    break;
            }
            itemList.Add(item);

        }
        

    }
    public Item GetItemById(int id)
    {
        foreach(Item item in itemList)
        {
            if(item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    public void ShowToolTip(string str,Vector3 pos)
    {
        toolTip.Show(str, pos);
    }
    public void ShowAutoFollowToolTip(string str)
    {
        toolTip.Show(str);
    }
    public void HideToolTip()
    {
        toolTip.Hide();
    }
}
