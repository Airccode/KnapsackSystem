using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemUI : MonoBehaviour//,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    #region Data
    public Item item { get; set; }
    public int Amount { get; set; }
    private Vector3 originPosition;
    #endregion
    #region UI Component
    private Text text;
    private Image image;
    public RectTransform rectTransform;
    #endregion
    public bool IsFilled() { return Amount >= item.Capacity; }
    public void SetItem(Item item,int Amount = 1)
    {
        if(this.item == null)
        {
            this.item = item;
            this.Amount = Amount;
            //不能在Start()方法获取，直接在SetItem的时候获取
            //利用Instantiate调用不会调用Start()，也可以利用引用，引用get的时候判断是否为空，为空就GetComponent()
            image = GetComponent<Image>();
            text = GetComponentInChildren<Text>();
            rectTransform = GetComponent<RectTransform>();
            image.sprite = Resources.Load<Sprite>(item.Sprite);
        }
       
        text.text = this.Amount.ToString();
        
        //Update UI TODO
    }
    public void AddAmount(int amount = 1)
    {
        this.Amount += amount;
        text.text = this.Amount.ToString();
        //Update UI TODO
    }
    public void UpdateUI()
    {
        image.sprite = Resources.Load<Sprite>(item.Sprite);
        text.text = this.Amount.ToString();
    }
    public void CloseRayCast()
    {
        Debug.Log("close");
        image.raycastTarget = false ;
    }
    public void OpenRayCast()
    {
        Debug.Log("open");
        image.raycastTarget = true;
    }
    //private List<RaycastResult> GraphicRaycaster(Vector2 pos)

    //{

    //    var mPointerEventData = new PointerEventData(CanvasManager.Instance._evenSystem);

    //    mPointerEventData.position = pos;

    //    List<RaycastResult> results = new List<RaycastResult>();

    //    CanvasManager.Instance.gry.Raycast(mPointerEventData, results);

    //    return results;

    //}

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    originPosition = this.transform.position;
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    List<RaycastResult> list = GraphicRaycaster(Input.mousePosition);
    //    bool isSlot = false;
    //    Vector3 slotPosition = new Vector3();
    //    //如果有Slot无论是否有Item都要将当前的position更新到这个Slot中
    //    //所以如果有Slot，记录出这个Slot的position，再更新Position
    //    //有Slot 无Item，代表空Slot，可以放入Slot所在position
    //    //有Slot 有Item，交换两个Item，依然可以放入Slot所在position

    //    //注意交换两个ItemUI对应的gameObject的父亲关系
    //    Transform parent = this.transform.parent;
    //    foreach (RaycastResult hit in list)
    //    {
    //        if (hit.gameObject.tag == "Slot")
    //        {

    //            isSlot = true;
    //            if(hit.gameObject.transform.childCount > 0)
    //            {
    //                //Transform obj = hit.gameObject.transform.GetChild(0);
    //                hit.gameObject.transform.GetChild(0).position = originPosition;
    //                hit.gameObject.transform.GetChild(0).SetParent(this.transform.parent);
    //            }
    //            this.transform.SetParent(hit.gameObject.transform);

    //            slotPosition = hit.gameObject.transform.position;

    //        }
    //    }
    //    if (isSlot)
    //    {
    //        this.transform.position = slotPosition;

    //    }
    //    else
    //    {
    //        this.transform.position = originPosition;
    //    }

    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    rectTransform.anchoredPosition += eventData.delta;
    //}
}
