using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private static CanvasManager _instance;
    public static CanvasManager Instance
    {
        get
        {
            if(_instance == null)
            {
                return GameObject.Find("Canvas").GetComponent<CanvasManager>();
            }
            return _instance;
        }
    }
    private RectTransform rectTransform;
    public EventSystem _evenSystem;
    public GraphicRaycaster gry;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        _evenSystem = FindObjectOfType<EventSystem>();
        gry = GetComponent<GraphicRaycaster>();
    }
    public RectTransform GetRectTransform() { return rectTransform; }

}
