using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject keeper;

    [SerializeField] private GameObject rightDiaPrefab;
    [SerializeField] private GameObject leftDiaPrefab;
    [SerializeField] private GameObject rightDiaWithPortPrefab;
    [SerializeField] private GameObject leftDiaWithPortPrefab;

    public static DialogueManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    //���� singleton���õȰ� �����ؾߵ�

    /// <summary>
    /// �̹����� ������ �ʴ� �޼����� ���
    /// </summary>
    /// <param name="isRight">Right, �� ���� ������ ���̸� true</param>
    /// <param name="content"></param>
    public void addDialogue(bool isRight, string content)
    {
        GameObject temp = null;
        if (isRight)
        {
            temp = Instantiate(rightDiaPrefab, keeper.transform);
            
        }
        else
        {
            temp = Instantiate(leftDiaPrefab, keeper.transform);
        }

        temp.GetComponent<DialogueWOPort>().setDialogue(content);
        scrollRect.content.localPosition = SnapTo(temp.GetComponent<RectTransform>());
    }

    /// <summary>
    /// �̹����� ������ �޼����� ���
    /// </summary>
    /// <param name="imgName"></param>
    /// <param name="content"></param>
    /// <param name="isRight">Right, �� ���� ������ ���̸� true</param>
    public void addDialogue(string imgName, bool isRight, string content)
    {
        GameObject temp = null;
        if (isRight)
        {
            temp = Instantiate(rightDiaWithPortPrefab, keeper.transform);
        }
        else
        {
            temp = Instantiate(leftDiaWithPortPrefab, keeper.transform);
        }
        temp.GetComponent<DialogueWPort>().setDialogue(imgName, content);
        scrollRect.content.localPosition = SnapTo(temp.GetComponent<RectTransform>());
    }

    public void clearDialogue()
    {
        foreach (Transform child in keeper.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private Vector2 SnapTo(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();
        Vector2 viewportLocalPos = scrollRect.viewport.localPosition;
        Vector2 childLocalPos = target.localPosition;
        Vector2 result = new Vector2(
            0 - (viewportLocalPos.x + childLocalPos.x),
            0 - (viewportLocalPos.y + childLocalPos.y));

        return result;
    }
}
