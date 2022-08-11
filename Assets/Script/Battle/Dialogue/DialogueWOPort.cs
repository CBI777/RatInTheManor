using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueWOPort : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt;
    public void setDialogue(string wantedText)
    {
        this.txt.SetText(wantedText);
    }
}
