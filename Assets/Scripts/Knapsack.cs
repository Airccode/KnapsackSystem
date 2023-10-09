using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Knapsack : MonoBehaviour,IEndDragHandler
{
    #region 单例模式
    private static Knapsack _instance;
    public static Knapsack Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Knapsack").GetComponent<Knapsack>();
            }
            return _instance;
        }
    }


    #endregion

    private void Start()
    {
        
        slotPageManager = transform.GetComponentInChildren<SlotPageManager>();
        scrollRect = GetComponent<ScrollRect>();
        pageIndex = 0;
        Pages = slotPageManager.PageCount;
        SetPage(pageIndex);
        smooth = 0.1f;
    }
    private SlotPageManager slotPageManager;
    private ScrollRect scrollRect;
    private int Pages;
    private int pageIndex;
    public float smooth;
    //协程管理，防止协程动画的时候进行拖拽，导致出现异常scrollRect.horizontalNormalizedPosition的情况
    private int coroutineCounter;
    public int slotCount(int Pages)
    {
        return slotPageManager.GetSlotCount(Pages);
    }
    public int pageCount()
    {
        return Pages;
    }
    public void ScrollNextPage()
    {
        pageIndex = (int)(pageIndex + 1) % pageCount();
        SetPage(pageIndex);
    }
    public void ScollPrePage()
    {
        pageIndex = (int)(pageIndex - 1 + pageCount()) % pageCount();
        SetPage(pageIndex);
    }
    public bool SetPage(int page)
    {
        if(page < 0 || page > pageCount()) { return false; }
        scrollRect.horizontalNormalizedPosition = (float)page / (pageCount() - 1);
        Debug.Log(page);
        Debug.Log(pageCount());
        Debug.Log(scrollRect.horizontalNormalizedPosition);
        return true;
    }
    public bool StoreItem(Item item)
    {
        return slotPageManager.StoreItem(item);
    }
    public bool StoreItem(int ID)
    {
        return slotPageManager.StoreItem(ID);
    }

    public bool StoreItem(int ID,int Page,int slotIndex)
    {
        return slotPageManager.StoreItem(ID,Page,slotIndex);
    }
    public bool AddItem(int ID)
    {
        return slotPageManager.AddItem(ID);
    }
    public bool AddItem(int ID,int Page)
    {
        return slotPageManager.AddItem(ID,Page);
    }

    IEnumerator LerpHorizontal(float end)
    {
        yield return coroutineCounter++;
        while(Mathf.Abs(end - scrollRect.horizontalNormalizedPosition) > 0.001)
        {
            yield return scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, end, smooth);
        }
        yield return scrollRect.horizontalNormalizedPosition = end;
        yield return coroutineCounter--;
    }
    public void OnStartDrag(PointerEventData eventData)
    {
        if (coroutineCounter > 0) { return; }
        scrollRect.OnBeginDrag(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (coroutineCounter > 0) { return; }
        scrollRect.OnDrag(eventData);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (coroutineCounter > 0) { return; }
        float start = scrollRect.horizontalNormalizedPosition;
        float target = (float)Mathf.RoundToInt(start * (pageCount() - 1)) / (pageCount() - 1);
        StartCoroutine(LerpHorizontal(target));
    }
}
