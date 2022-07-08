using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TokenDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    public Transform returnParent;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void checkSlot()
    {
        if (this.returnParent.GetComponent<NormalTokenSlot>() != null)
        {
            this.returnParent.GetComponent<NormalTokenSlot>().slotCountArrange();
        }
    }

    //OnPointerDown->OnBegineDrag->OnDrag->OnDrop->OnEndDrag

    //토큰을 끌기 시작할 때
    public void OnBeginDrag(PointerEventData eventData)
    {
        this.returnParent = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        checkSlot();
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
        this.transform.SetParent(this.returnParent);
        checkSlot();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    //토큰 클릭(잡으려고) 할 때
    public void OnPointerDown(PointerEventData eventData)
    {
    }
}
