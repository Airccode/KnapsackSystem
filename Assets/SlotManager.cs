using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    protected Slot[] slotList;
    public int slotCount;

    public virtual void Start()
    {
        slotList = GetComponentsInChildren<Slot>();
        slotCount = slotList.Length;
    }

    public bool StoreItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        //Debug.Log(id);
        return StoreItem(item);
    }
    public bool StoreItem(Item item)
    {
        if (item == null)
        {
            Debug.LogWarning("要存储的物体ID不存在");

            return false;
        }
        bool isStore = false;
        if (item.Capacity != 1)
        {
            Slot slot = FindEmptySlot();
            if (slot != null)
            {
                isStore = true;
                slot.StoreItem(item);//把物体存储到空的物体槽
            }
        }
        else
        {
            Slot slot = FindSameTypeSlot(item);
            if (slot != null && !slot.IsFilled())
            {
                isStore = true;
                slot.StoreItem(item);
            }
            else
            {
                Slot emptySlot = FindEmptySlot();
                if (emptySlot != null)
                {
                    isStore = true;
                    emptySlot.StoreItem(item);
                }     
            }
        }
        return isStore;


    }
    public bool AddItem(int ID)
    {
        Item item = InventoryManager.Instance.GetItemById(ID);
        return AddItem(item);
    }

    public bool AddItem(Item item)
    {
        bool isStore = false;
        if (item == null)
        {
            Debug.LogWarning("要存储的物体ID不存在");
            return false;
        }
        if (item.Capacity == 1)
        {
            Slot slot = FindEmptySlot();
            if (slot != null)
            {
                isStore = true;
                slot.StoreItem(item);//把物体存储到空的物体槽
            }
        }
        else
        {
            Slot emptySlot = FindEmptySlot();
            if (emptySlot != null)
            {
                isStore = true;
                emptySlot.StoreItem(item);
            }
        }
        return isStore;
    }

    public bool StoreItem(int ID, int index)
    {
        Item item = InventoryManager.Instance.GetItemById(ID);
        return StoreItem(item, index);

    }
    public bool StoreItem(Item item,int index)
    {
        if (item == null)
        {
            Debug.LogWarning("要存储的物体ID不存在");
            return false;
        }
        if ((slotList[index].transform.childCount != 0 && slotList[index].GetItemID() != item.ID))
        {
            Debug.LogWarning(string.Format("当前Index:{0}的类型与存储的类型不匹配", index));
            return false;
        }
        if ((slotList[index].transform.childCount != 0 && slotList[index].GetItemID() == item.ID && slotList[index].IsFilled()))
        {
            Debug.LogWarning(string.Format("当前Index:{0}没有空位", index));
            return false;
        }
        slotList[index].StoreItem(item);

        return true;
    }
    /// <summary>
    /// 找空物体槽
    /// </summary>
    /// <returns>返回空物体槽</returns>
    public Slot FindEmptySlot()
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }
    public Slot FindSameTypeSlot(Item item)
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount > 0 && slot.GetItemID() == item.ID && !slot.IsFilled())
            {
                return slot;
            }
        }
        return null;
    }

    public int GetSlotCount() { return slotList.Length; }
    
    public Item GetItem(int index)
    {
        if (slotList[index].transform.childCount > 0)
        {
            return slotList[index].transform.GetChild(0).gameObject.GetComponent<ItemUI>().item;
        }
        return null;
    }
    
}
