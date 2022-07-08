using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalDmgChange : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myText;
    private void OnEnable()
    {
        myText = this.transform.GetComponent<TextMeshProUGUI>();
        SlotManager.TotalDmgChanged += SlotManager_TotalDmgChanged;
    }

    private void OnDisable()
    {
        SlotManager.TotalDmgChanged -= SlotManager_TotalDmgChanged;
    }

    private void SlotManager_TotalDmgChanged(int obj)
    {
        myText.SetText(obj.ToString());  
    }
}
