using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueWPort : MonoBehaviour
{
    [SerializeField] private Image portrait;
    [SerializeField] private TextMeshProUGUI txt;

    public void setDialogue(string imgName, string wantedText)
    {
        this.portrait.sprite = Resources.Load<Sprite>("Sprite/Portrait/" + imgName);
        this.txt.SetText(wantedText);
    }
}
