using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Title_UI : MonoBehaviour
{
    [SerializeField] SaveM_Title saveManager;
    [SerializeField] GameObject newGameConfirm;
    [SerializeField] GameObject blackOut;

    [SerializeField] Button newGameBtn;
    [SerializeField] Button resumeBtn;

    [SerializeField] ButtonSFX btnsfx;

    public static event Action StartNewGame;
    public static event Action ResumeGame;

    private bool isNew = false;

    private void Start()
    {
        if(saveManager.saving.stageNum == -2)
        {
            isNew = true;
            resumeBtn.interactable = false;
        }
    }

    IEnumerator BackGroundFade(bool isNewGame)
    {
        blackOut.gameObject.SetActive(true);
        Color c = new Color(0f, 0f, 0f, 0f);
        for (int i = 0; i <= 100; i++)
        {
            c.a = i / 100f;
            blackOut.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.01f);
        }

        if(isNewGame)
        {
            StartNewGame?.Invoke();
        }
        else
        {
            ResumeGame?.Invoke();
        }
    }

    public void StartButtonConfirmed()
    {
        saveManager.Title_UI_StartNewGame();
        newGameConfirm.gameObject.SetActive(false);
        StartCoroutine(BackGroundFade(true));
    }

    public void ResumeButtonClicked()
    {
        StartCoroutine(BackGroundFade(false));
    }

    public void StartButtonClicked()
    {
        //saving을 받아와서, turn이 -2라면 title로 친다. newgame을 click하면 -1로 바꾸고.
        if (isNew)
        {
            SoundEffecter.playSFX("DoorOpen");
            StartCoroutine(BackGroundFade(true));
        }
        else
        {
            SoundEffecter.playSFX("BeepYes");
            newGameConfirm.gameObject.SetActive(true);
        }
    }
}
