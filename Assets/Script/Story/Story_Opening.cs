using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Story_Opening : MonoBehaviour
{
    [SerializeField] private List<RectTransform> backgrounds; //���丮 �����̵�.
    [SerializeField] private List<string> sfxName;
    [SerializeField] private Button skipBtn;
    private int i = 100; //���̵�� ����
    private int index = 0;//���� ��� �����̵�
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private GameObject blackOut;

    public static event Action StartGame;

    public void SkipStory() //���丮 ��ŵ
    {
        StartCoroutine(BackGroundFade());
    }
    public void ImageClicked() //��⺯ȭ�� ��� Ŭ���ϸ� ȿ�� ��ŵ ����, ȿ�� ���� �� Ŭ���ϸ� ���� �����̵�
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
    IEnumerator BackGroundFadeChange() //��⺯ȭ �� ���丮 �����̵�(?) �ѱ��
    {
        audioSource.Stop();
        RectTransform prev, cur;
        if (index > -1)
        {
            prev = backgrounds[index];
            prev.transform.SetAsLastSibling();
        }
        if (sfxName[index] != "a")
        {
            audioSource.PlayOneShot(Resources.Load<AudioClip>("SFX/Story/" + sfxName[index]));
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
        audioSource.Stop();
        blackOut.gameObject.SetActive(true);
        Color c = new Color(0f, 0f, 0f, 0f);
        for (int i = 0; i <= 200; i++)
        {
            c.a = i / 200f;
            blackOut.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.01f);
        }
        StartGame?.Invoke();
    }
}
