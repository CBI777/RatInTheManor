using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Story_BadEnd : MonoBehaviour
{
    [SerializeField] private List<RectTransform> backgrounds; //스토리 슬라이드.
    [SerializeField] private Button skipBtn;
    private int i = 100; //페이드용 변수
    private int index = 0;//현재 몇번 슬라이드

    [SerializeField] private GameObject blackOut;
    [SerializeField] private GameObject Ending;

    public void SkipStory() //스토리 스킵
    {
        StartCoroutine(BackGroundFade());
    }
    public void ImageClicked() //밝기변화중 배경 클릭하면 효과 스킵 가능, 효과 끝난 후 클릭하면 다음 슬라이드
    {
        if (i >= 100)
        {
            if (index < backgrounds.Count)
            {
                StartCoroutine(BackGroundFadeChange());
            }
            else
            {
                skipBtn.interactable = false;
                StartCoroutine(BackGroundFade());
            }
        }
        else
        {
            i = 99;
        }
    }
    IEnumerator BackGroundFadeChange() //밝기변화 및 스토리 슬라이드(?) 넘기기
    {
        RectTransform prev, cur;
        if (index > -1)
        {
            prev = backgrounds[index];
            prev.transform.SetAsLastSibling();
        }
        cur = backgrounds[index++];
        cur.transform.SetAsLastSibling();
        cur.gameObject.SetActive(true);
        float tc = 0f;
        for (i = 0; i <= 100; i++)
        {
            tc = i / 100f;
            cur.GetComponentInChildren<TextMeshProUGUI>().alpha = tc;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator BackGroundFade()
    {
        blackOut.gameObject.SetActive(true);
        Color c = new Color(0f, 0f, 0f, 0f);
        for (int i = 0; i <= 200; i++)
        {
            c.a = i / 200f;
            blackOut.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.01f);
        }
        Ending.SetActive(true);
    }
}
