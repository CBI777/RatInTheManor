using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;

public class Story_Begin : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myText;
    [SerializeField] GameObject blackOut;
    [SerializeField] RectTransform NewsPaper;
    [SerializeField] GameObject panel;

    private AudioSource audioSource;

    private Coroutine cor;

    public static event Action SafeToGo;

    private void OnEnable()
    {
        SaveM_Story.StorySaveComplete += SaveM_Story_StorySaveComplete;
    }

    private void OnDisable()
    {
        SaveM_Story.StorySaveComplete -= SaveM_Story_StorySaveComplete;
    }

    private void SaveM_Story_StorySaveComplete()
    {
        this.blackOut.transform.SetAsLastSibling();
        blackOut.gameObject.SetActive(true);
        StartCoroutine(FinishIt());
    }

    private void Awake()
    {
        this.audioSource = transform.GetComponent<AudioSource>();
    }

    IEnumerator VolDownToEnd()
    {
        yield return new WaitForSeconds(5.3f);
        float v = this.audioSource.volume;
        do
        {
            v -= 0.001f;
            this.audioSource.volume = v;
            yield return new WaitForSeconds(0.01f);

        } while (v >= 0.3);

        this.audioSource.Stop();
    }

    public void MoveNP()
    {
        NewsPaper.DOAnchorPos(new Vector2(0, -110f), 3f);
        StartCoroutine(BackGroundFade());
        StartCoroutine(VolDownToEnd());
        StopCoroutine(cor);
        myText.transform.gameObject.SetActive(false);
    }

    IEnumerator FinishIt()
    {
        Color c = new Color(0f, 0f, 0f, 0f);
        for (int i = 0; i <= 200; i++)
        {
            c.a = i / 200f;
            blackOut.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.01f);
        }
        SafeToGo?.Invoke();
    }

    IEnumerator BackGroundFade()
    {
        yield return new WaitForSeconds(5.3f);
        blackOut.gameObject.SetActive(true);
        Color c = new Color(0f, 0f, 0f, 0f);
        for (int i = 0; i <= 300; i++)
        {
            c.a = i / 300f;
            blackOut.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1.5f);
        panel.gameObject.SetActive(true);
    }

    IEnumerator BackGroundLight()
    {
        Color c = new Color(0f, 0f, 0f, 0f);
        for (int i = 400; i >= 0; i--)
        {
            c.a = i / 400f;
            blackOut.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.01f);
        }
        blackOut.gameObject.SetActive(false);
    }

    IEnumerator ButtonBlink()
    {
        Color c = new Color(1f, 1f, 1f, 1f);
        while(true)
        {
            for (int i = 200; i >= 0; i--)
            {
                c.a = i / 200f;
                myText.color = c;
                yield return new WaitForSeconds(0.01f);
            }
            for (int i = 0; i <= 200; i++)
            {
                c.a = i / 200f;
                myText.color = c;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    private void Start()
    {
        StartCoroutine(BackGroundLight());
        cor = StartCoroutine(ButtonBlink());
    }
}
