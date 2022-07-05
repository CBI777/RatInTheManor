using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TokenSlot : MonoBehaviour, IDropHandler
{
    public enum SlotType
    {
        Hand,
        Resist,
        Equipment,
        Enemy
    };
    [SerializeField] private SlotType slotType;
    [SerializeField] private GameObject textObject;
    [SerializeField] private bool slotEnabled;
    private TMP_Text matchingText;
    private int tokenCount = 0;

    //턴의 시작에 놓여진 토큰 갯수를 체크하는 함수임
    //즉, 뭐 토큰이 놓이는 일이 있다면(완전방어) 그 토큰을 놓아둔 뒤에 이걸 부르면 됨.
    public void initSlot()
    {
        this.tokenCount = this.transform.childCount;
        this.matchingText = textObject.GetComponent<TMP_Text>();
        matchingText.text = tokenCount.ToString();
    }

    public void Start()
    {
        //Temporary
        initSlot();
    }

    //토큰이 슬롯 위에서 onEndDrag되었을 경우
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && slotEnabled)
        {
            Debug.Log("On Drop" + eventData.pointerDrag.name);
            eventData.pointerDrag.GetComponent<TokenDragDrop>().newParent = this.transform;
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
        switch (slotType)
        {
            case SlotType.Enemy:
            case SlotType.Hand:
                break;
            case SlotType.Resist:
                this.tokenCount++;
                matchingText.text = tokenCount.ToString();
                break;
            case SlotType.Equipment:
                //TODO
                break;
            default:
                break;
        }
    }

    public void OnBeginDrag()
    {
        if (slotType == SlotType.Resist)
        {
            this.tokenCount--;
            matchingText.text = tokenCount.ToString();
        }
    }

    public void OnCancelDrag()
    {
        if (slotType == SlotType.Resist)
        {
            this.tokenCount++;
            matchingText.text = tokenCount.ToString();
        }
    }
}
