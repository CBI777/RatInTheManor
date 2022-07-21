using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class QuirkManager : MonoBehaviour
{
    private int quirkLimit = 5;

    private List<Quirk_Base> player_will = new List<Quirk_Base>();
    private List<Quirk_Base> player_feeble = new List<Quirk_Base>();

    private int willCount;
    private int feebleCount;

    public static event Action<int, Quirk_Base[]> WillChangedEvent;
    public static event Action<int, Quirk_Base[]> FeebleChangedEvent;

    private void OnEnable()
    {
        //action += onTurnStart
    }

    private void OnDisable()
    {
        //action -= onTurnStart
    }

    private void AddWill(string name)
    {
        if(this.willCount == quirkLimit)
        {
            this.player_will.RemoveAt(UnityEngine.Random.Range(0, willCount));
        }

        this.player_will.Add((Quirk_Base)Activator.CreateInstance(Type.GetType(name)));
        this.willCount = this.player_will.Count;
        this.player_will[willCount - 1].onObtain();
        WillChangedEvent?.Invoke(willCount, player_will.ToArray());
    }

    private void RemoveWill()
    {
        int temp = UnityEngine.Random.Range(0, willCount);
        if (willCount == 0) { return; }

        this.player_will[temp].onLose();
        this.player_will.RemoveAt(temp);
        this.willCount = this.player_will.Count;
        WillChangedEvent?.Invoke(willCount, player_will.ToArray());
    }

    private void RemoveWill(int n)
    {
        if (willCount == 0) { return; }

        if (n > quirkLimit || n < 0) { return; }

        this.player_will[n].onLose();
        this.player_will.RemoveAt(n);
        this.willCount = this.player_will.Count;
        WillChangedEvent?.Invoke(willCount, player_will.ToArray());
    }

    private void AddFeeble(string name)
    {
        if (this.feebleCount == quirkLimit)
        {
            this.player_feeble.RemoveAt(UnityEngine.Random.Range(0, feebleCount));
        }

        this.player_feeble.Add((Quirk_Base)Activator.CreateInstance(Type.GetType(name)));
        this.feebleCount = this.player_feeble.Count;
        this.player_feeble[feebleCount - 1].onObtain();
        FeebleChangedEvent?.Invoke(feebleCount, player_feeble.ToArray());
    }

    private void RemoveFeeble()
    {
        int temp = UnityEngine.Random.Range(0, feebleCount);
        if (feebleCount == 0) { return; }

        this.player_feeble[temp].onLose();
        this.player_feeble.RemoveAt(temp);
        this.feebleCount = this.player_feeble.Count;
        FeebleChangedEvent?.Invoke(feebleCount, player_feeble.ToArray());
    }

    private void RemoveFeeble(int n)
    {
        if (feebleCount == 0) { return; }
        if (n > quirkLimit || n < 0) { return; }

        this.player_feeble[n].onLose();
        this.player_feeble.RemoveAt(n);
        this.feebleCount = this.player_feeble.Count;
        FeebleChangedEvent?.Invoke(feebleCount, player_feeble.ToArray());
    }

    private void Start()
    {
        AddWill("Quirk_BasicUpPhys");
        AddWill("Quirk_BasicUpAbhorr");
        AddWill("Quirk_BasicUpDelus");
        RemoveWill();

        AddFeeble("Quirk_BasicDownFear");
        AddFeeble("Quirk_BasicDownDelus");
        AddFeeble("Quirk_BasicDownPhys");
        AddFeeble("Quirk_BasicDownAbhorr");
        RemoveFeeble();
    }

}
