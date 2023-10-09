using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public GameObject itemPrefab;
    private ItemUI itemUI;
    private Vector3 originPosition;
    bool isDragEmptySlot;
    /// <summary>
    /// 把Item放到自身下面
    /// 如果已有Item则自加
    /// 否则直接放下面
    /// </summary>
    /// <param name="item"></param>
    ///
    
    public void StoreItem(Item item,int Amount = 1)
    {
        if(Amount == -1)
        {
            //TODO
            
        }
        else
        {
            if(itemUI == null)
            {
                Init(item);
            }
            else
            {
                itemUI.AddAmount(Amount);
            }
        }
            
        
    }
    public void ChangeItem(ItemUI otherItemUI)
    {
        if(otherItemUI == null)
        {
            itemUI = null;
            return;
        }
        (itemUI, otherItemUI) = (otherItemUI, itemUI);
        itemUI.UpdateUI();
        if(otherItemUI != null)
        {
            otherItemUI.UpdateUI();
        }
    }
    private void Init(Item item)
    {
        if(item == null)
        {
            itemUI = null;
            return;
        }
        GameObject itemGameObject = Instantiate(itemPrefab) as GameObject;
        Debug.Log(itemGameObject.transform.localScale);
        itemGameObject.transform.SetParent(this.transform);
        itemGameObject.transform.localPosition = Vector3.zero;
        itemUI = itemGameObject.GetComponent<ItemUI>();
        itemUI.SetItem(item);
    }


    /// <summary>
    /// 得到当前物体槽的存储类型
    /// 需要自行判断slot子节点Item是否存在，存在的话itemUI肯定存在
    /// </summary>
    /// <returns></returns>
    public Item.ItemType GetItemType()
    {
        return itemUI.item.Type;
    }
    /// <summary>
    /// 判断Slot下面的Item是否满了
    /// 没有判断Slot是否有Item，需自行判断
    /// </summary>
    /// <returns></returns>
    public bool IsFilled()
    {
        return itemUI.IsFilled();
    }

    public int GetItemID()
    {
        return itemUI.item.ID;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemUI == null) {return; }
        //Vector3 pos = transform.position;
        //pos = pos + Vector3.right * rectTransform.rect.width / 2 + Vector3.down * rectTransform.rect.height / 2;
        //Debug.Log(pos);
        //InventoryManager.Instance.ShowToolTip(itemUI.item.GetItemDescription(), pos);
        isDragEmptySlot = false;
        InventoryManager.Instance.ShowAutoFollowToolTip(itemUI.item.GetItemDescription());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(itemUI == null) { return; }
        InventoryManager.Instance.HideToolTip();
    }

    private List<RaycastResult> GraphicRaycaster(Vector2 pos)
    {

        var mPointerEventData = new PointerEventData(CanvasManager.Instance._evenSystem);

        mPointerEventData.position = pos;

        List<RaycastResult> results = new List<RaycastResult>();

        CanvasManager.Instance.gry.Raycast(mPointerEventData, results);

        return results;

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if(itemUI == null) {isDragEmptySlot = true;Knapsack.Instance.OnStartDrag(eventData); return; }
        isDragEmptySlot = false;
        itemUI.CloseRayCast();
        originPosition = this.transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragEmptySlot) { Knapsack.Instance.OnEndDrag(eventData); return; }
        List<RaycastResult> list = GraphicRaycaster(Input.mousePosition);
        itemUI.OpenRayCast();
        Vector3 slotPosition = new Vector3();
        GameObject slot = null;

        //先判断是否有相同物体，如果有相同物体不需要交换position，修改数量即可
        //假设拖动的是B
        //如果A + B <= Capacity，将A和B合并，删除一个GameObjcet
        //如果A + B > Capacity，如果A的Amount到达Capciity，则将A的数量加到Capacity，否则两者交换
        foreach (RaycastResult hit in list)
        {
            //RaycastResult就两个，一个是Item，一个是Slot
            
            if (hit.gameObject.tag == "Slot")
            {
                slot = hit.gameObject;
                if (slot.gameObject.transform.childCount != 0)
                {
                    GameObject item = slot.gameObject.transform.GetChild(0).gameObject;
                    ItemUI otherUI = item.GetComponent<ItemUI>();
                    if (otherUI.item.ID == this.itemUI.item.ID && item != this.gameObject)
                    {
                        int Capacity = itemUI.item.Capacity;
                        //A的容量不满，需要处理数字，如果满了后续直接交换就行
                        if (otherUI.Amount != Capacity)
                        {
                            int dAmount = Mathf.Clamp(otherUI.Amount + this.itemUI.Amount, 0, Capacity) - itemUI.Amount;
                            itemUI.AddAmount(dAmount);
                            if (otherUI.Amount <= dAmount)
                            {
                                Destroy(otherUI.gameObject);
                            }
                            else
                            {
                                otherUI.AddAmount(-dAmount);
                                itemUI.transform.position = originPosition;
                            }
                        }

                        
                    }
                }
                
                //记录slot对应的gameObject，用于后面交换
                
            }
        }

        if (slot != null)
        { 
            if (slot.transform.childCount > 0)
            {
                GameObject item = slot.transform.GetChild(0).gameObject;
                item.transform.SetParent(this.transform);
                item.transform.position = originPosition;
            }
            this.itemUI.rectTransform.position = slot.transform.position;
            this.transform.GetChild(0).transform.SetParent(slot.transform);
            ItemUI otherItemUI = slot.GetComponent<Slot>().itemUI;
            (itemUI, slot.GetComponent<Slot>().itemUI) = (slot.GetComponent<Slot>().itemUI, itemUI);
        }
        else
        {
            this.itemUI.rectTransform.position = originPosition;
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragEmptySlot) { Knapsack.Instance.OnDrag(eventData); return; }
        itemUI.transform.position += new Vector3(eventData.delta.x,eventData.delta.y,0.0f);
        //itemUI.rectTransform.anchoredPosition += eventData.delta;
    }

}
