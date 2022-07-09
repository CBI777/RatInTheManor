using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class TokenDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    public Transform returnParent;
    private Transform beforeParent;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    [SerializeField] private bool dragable = true;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        this.beforeParent = this.transform.parent;
        this.returnParent = this.transform.parent;
    }
    private void OnDisable()
    {
        
    }

    private void tokenRemoveCheck()
    {
        this.returnParent.SendMessage("tokenRemoved");
    }

    private void tokenAddCheck()
    {
        this.returnParent.SendMessage("tokenAdded");
    }

    private void tokenAfterRemoveCheck()
    {
        this.beforeParent.SendMessage("tokenAfterRemoved");
    }

    //OnPointerDown->OnBeginDrag->OnDrag->OnDrop->OnEndDrag

    //토큰을 끌기 시작할 때
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!this.dragable) { return; }

        this.returnParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        tokenRemoveCheck();
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    //토큰을 끌고 있을 때
    public void OnDrag(PointerEventData eventData)
    {
        if (!this.dragable) { return; }

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    //토큰을 drop한 후
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!this.dragable) { return; }

        this.transform.SetParent(this.returnParent);
        tokenAddCheck();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if(this.returnParent != this.beforeParent)
        {
            tokenAfterRemoveCheck();
        }
        this.beforeParent = this.transform.parent;
    }

    //토큰 클릭(잡으려고) 할 때
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!this.dragable) { return; }
    }

    public void Undragable()
    {
        this.dragable = false;
        Color clr;
        ColorUtility.TryParseHtmlString("#003E68", out clr);
        this.transform.GetComponent<Image>().color = clr;
    }

    public void Dragable()
    {
        this.dragable = true;
        Color clr;
        ColorUtility.TryParseHtmlString("#0694F5", out clr);
        this.transform.GetComponent<Image>().color = clr;
    }
}
