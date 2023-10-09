using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    protected Slot[] slotList;
    

    public virtual void Start()
    {
        slotList = GetComponentsInChildren<Slot>();
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
        if(item.Capacity == 1)
        {
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.LogWarning("没有空的物体槽");
                return false;
            }
            slot.StoreItem(item);//把物体存储到空的物体槽
        }
        else
        {
            Slot slot = FindSameTypeSlot(item);
            if(slot != null && !slot.IsFilled())
            {
                slot.StoreItem(item);
            }
            else
            {
                Slot emptySlot = FindEmptySlot();
                if(emptySlot == null)
                {
                    Debug.LogWarning("没有空的物品槽");
                    return false;
                }
                emptySlot.StoreItem(item);
            }
        }
        return true;


    }
    public bool AddItemTest(int ID)
    {
        Item item = InventoryManager.Instance.GetItemById(ID);
        if (item == null)
        {
            Debug.LogWarning("要存储的物体ID不存在");

            return false;
        }
        if (item.Capacity == 1)
        {
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.LogWarning("没有空的物体槽");
                return false;
            }
            slot.StoreItem(item);//把物体存储到空的物体槽
        }
        else
        {
            
           Slot emptySlot = FindEmptySlot();
           if (emptySlot == null)
           {
                Debug.LogWarning("没有空的物品槽");
                return false;
            }
            emptySlot.StoreItem(item);
            
        }
        return true;
    }
    public bool StoreItem(int ID,int index)
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
    private Slot FindEmptySlot()
    {
        foreach(Slot slot in slotList)
        {
            if(slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }
    private Slot FindSameTypeSlot(Item item)
    {
        foreach(Slot slot in slotList)
        {
            if(slot.transform.childCount > 0 && slot.GetItemID() == item.ID && !slot.IsFilled())
            {
                return slot;
            }
        }
        return null;
    }
    public int GetSlotCount() { return slotList.Length; }

}
