using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TokenDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    public Transform returnParent;
    public Transform newParent;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //OnPointerDown->OnBegineDrag->OnDrag->OnDrop->OnEndDrag

    //토큰을 끌기 시작할 때
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        this.transform.parent.GetComponent<TokenSlot>().OnBeginDrag();
        this.newParent = null;
        this.returnParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    //토큰을 끌고 있을 때
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    //토큰을 drop한 후
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        if(this.newParent == null)
        {
            this.transform.SetParent(returnParent);
            this.transform.parent.GetComponent<TokenSlot>().OnCancelDrag();
        }
        else
        {
            this.transform.SetParent(newParent);
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    //토큰 클릭(잡으려고) 할 때
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
}
