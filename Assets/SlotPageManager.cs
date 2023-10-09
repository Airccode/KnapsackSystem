using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class SlotPageManager : MonoBehaviour
{
    //public SlotManager[] slotPage;
    public List<SlotManager> slotPage;
    private int pageCount;
    public int PageCount { get { return pageCount; } }
    private void Start()
    {
        slotPage = new List<SlotManager>(transform.GetComponentsInChildren<SlotManager>());
        pageCount = transform.childCount;
    }
    public int GetSlotCount(int Page)
    {
        if(Page > pageCount)
        {
            return -1;
        }
        return slotPage[Page].slotCount;
    }
    

    //Pass Item
    public bool StoreItem(Item item)
    {
        bool isStore = false; 
        for(int i = 0;i < pageCount; i++)
        {
            isStore = isStore || slotPage[i].StoreItem(item);
        }
        return isStore;
    }
    public bool StoreItem(Item item,int Page)
    {
        if (Page >= PageCount) { return false; }
        return slotPage[Page].StoreItem(item);
    }
    public bool StoreItem(Item item, int Page, int slotIndex)
    {
        if (Page >= PageCount) { return false; }
        return slotPage[Page].StoreItem(item, slotIndex);
    }

    //Pass ID
    public bool StoreItem(int ID)
    {
        bool isStore = false; ;
        for (int i = 0; i < pageCount; i++)
        {
            isStore = isStore || slotPage[i].StoreItem(ID);
        }
        return isStore;
    }
    public bool StoreItem(int ID, int Page)
    {
        if (Page >= PageCount) { return false; }
        return slotPage[Page].StoreItem(ID);
    }
    public bool StoreItem(int ID, int Page, int slotIndex)
    {
        if (Page >= PageCount) { return false; }
        return slotPage[Page].StoreItem(ID, slotIndex);
    }

    //ADD Pass Item
    public bool AddItem(Item item)
    {
        bool isStore = false; 
        for (int i = 0; i < pageCount; i++)
        {
            isStore = isStore || slotPage[i].AddItem(item);
        }
        return isStore;
    }
    public bool AddItem(Item item,int Page)
    {
        if (Page >= PageCount) { return false; }
        return slotPage[Page].AddItem(item);
    }

    //ADD Pass ID
    public bool AddItem(int ID)
    {
        bool isStore = false;
        for (int i = 0; i < pageCount; i++)
        {
            isStore = isStore || slotPage[i].AddItem(ID);
        }
        return isStore;
    }

    public bool AddItem(int ID,int Page)
    {
        if(Page >= PageCount) { return false; }
        return slotPage[Page].AddItem(ID);
    }

}
