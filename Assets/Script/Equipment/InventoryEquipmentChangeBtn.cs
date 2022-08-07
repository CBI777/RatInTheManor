using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

public class InventoryEquipmentChangeBtn : MonoBehaviour
{
    [SerializeField] private int equipNum;

    private UnityAction removeAction;
    private UnityAction changeAction;

    private AudioSource audioSource;
    [SerializeField] private AudioClip chngClip;

    public static event Action<int> curEquipChanged;

    private void OnEnable()
    {
        this.audioSource = this.transform.GetComponent<AudioSource>();
        removeAction = new UnityAction(removeInventoryEquipment);
        changeAction = new UnityAction(clickInventoryEquipment);
        gameObject.GetComponent<Button>().onClick.AddListener(changeAction);
        SkillManager.BattleStart += SkillManager_BattleStart;
        TurnManager.BattleEndEvent += TurnManager_BattleEnd;
    }

    private void OnDisable()
    {
        TurnManager.BattleEndEvent -= TurnManager_BattleEnd;
        SkillManager.BattleStart -= SkillManager_BattleStart;
    }

    private void TurnManager_BattleEnd()
    {
        gameObject.GetComponent<Button>().enabled = true;
    }

    private void SkillManager_BattleStart(int obj)
    {
        gameObject.GetComponent<Button>().enabled = false;
    }

    public void clickInventoryEquipment()
    {
        this.audioSource.PlayOneShot(chngClip);
        curEquipChanged?.Invoke(equipNum);
    }

    public void removeInventoryEquipment()
    {
        //TODO

    }

    /*
     * 
     * gameObject.GetComponent<Button>().onClick.RemoveListener(clickInventoryEquipment);
     * 
     */
}
