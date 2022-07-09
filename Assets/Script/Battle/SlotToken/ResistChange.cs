using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResistChange : MonoBehaviour
{
    [SerializeField] private int typeNum;
    [SerializeField] private TextMeshProUGUI myText;

    private void OnEnable()
    {
        myText = this.transform.GetComponent<TextMeshProUGUI>();
        Player.ResistChangedEvent += Player_ResistChangedEvent;
    }

    private void OnDisable()
    {
        Player.ResistChangedEvent -= Player_ResistChangedEvent;
    }

    private void Player_ResistChangedEvent(int[] obj)
    {
        switch (typeNum)
        {
            case 0:
                myText.SetText(obj[0].ToString());
                break;
            case 1:
                myText.SetText(obj[1].ToString());
                break;
            case 2:
                myText.SetText(obj[2].ToString());
                break;
            case 3:
                myText.SetText(obj[3].ToString());
                break;
        }
    }
}
