using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI headerField;
    [SerializeField]
    private TextMeshProUGUI contentField;
    [SerializeField]
    private LayoutElement layoutElement;
    [SerializeField]
    private int characterWrapLimit;
    [SerializeField]
    private RectTransform rectT;
    [SerializeField]
    private Canvas canvas;

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
    }

    private void Update()
    {
        Vector2 position = Input.mousePosition / canvas.scaleFactor;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectT.pivot = new Vector2(pivotX, pivotY);
        rectT.anchoredPosition = position;
    }
}
