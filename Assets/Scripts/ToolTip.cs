using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private Text toolTipText;
    private CanvasGroup canvasGroup;
    private float targetAlpha;
    public float smooth = 0.02f;
    private Coroutine followCoroutine;
    RectTransform rectTransform;
    RectTransform canvasTransform;
    public Vector3 deltaPosition = new Vector3(5,-10,0);
    private void Start()
    {
        toolTipText = GetComponentInChildren<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canvasTransform = CanvasManager.Instance.GetRectTransform();
    }
    IEnumerator LerpCoroutine()
    {
        while (Mathf.Abs(canvasGroup.alpha - targetAlpha) > 0.001)
        {
            yield return canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smooth);
        }
        yield return canvasGroup.alpha = targetAlpha;
    }
    IEnumerator FollowMouse()
    {

        while (true)
        {
            Vector2 mousePos;
            //获取要投影到的rectTransform，然后会返回该rectTransform的localPosition，选择鼠标坐标投影，输出对应的UI坐标
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, Input.mousePosition, null, out mousePos);
            rectTransform.localPosition = new Vector3(mousePos.x,mousePos.y,1.0f) + deltaPosition;
            //Debug.Log(mousePos);
            yield return null;
        }
    }
    public void Show(string text,Vector3 vec)
    {
        targetAlpha = 1;
        toolTipText.text = text;
        transform.position = vec;
        StartCoroutine(LerpCoroutine());
    }

    public void Show(string text)
    {
        targetAlpha = 1;
        toolTipText.text = text;
        StartCoroutine(LerpCoroutine());
        followCoroutine = StartCoroutine(FollowMouse());
    }
    public void Hide()
    {
        targetAlpha = 0;
        StartCoroutine(LerpCoroutine());
        if(followCoroutine != null)
            StopCoroutine(followCoroutine);
    }
}
