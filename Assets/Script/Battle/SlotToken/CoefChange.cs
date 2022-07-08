using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoefChange : MonoBehaviour
{
    [SerializeField] private int typeNum;
    [SerializeField] private TextMeshProUGUI myText;

    private void OnEnable()
    {
        myText = this.transform.GetComponent<TextMeshProUGUI>();
        SlotManager.CoefChanged += SlotManager_CoefChanged;
    }

    private void OnDisable()
    {
        SlotManager.CoefChanged -= SlotManager_CoefChanged;
    }

    private void SlotManager_CoefChanged(float arg1, float arg2, float arg3, float arg4)
    {
        switch (this.typeNum)
        {
            case 0:
                myText.SetText(arg1.ToString());
                break;
            case 1:
                myText.SetText(arg2.ToString());
                break;
            case 2:
                myText.SetText(arg3.ToString());
                break;
            case 3:
                myText.SetText(arg4.ToString());
                break;
        }
    }
}
