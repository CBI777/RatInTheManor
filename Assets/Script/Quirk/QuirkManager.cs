using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class QuirkManager : MonoBehaviour
{
    private int quirkLimit = 5;

    [SerializeField] private SaveM_Battle saveManager;

    [SerializeField] private ListOfItems willList;
    [SerializeField] private ListOfItems feebleList;

    public int[] getWillNum()
    {
        int[] willNum = new int[quirkLimit];
        for (int i = 0; i < this.quirkLimit; i++)
        {
            if (i >= this.willCount) { willNum[i] = -1; }
            else
            {
                willNum[i] = player_will[i].index;
            }
        }

        return willNum;
    }
    public int[] getFeebleNum()
    {
        int[] feebleNum = new int[quirkLimit];
        for (int i = 0; i < this.quirkLimit; i++)
        {
            if (i >= this.feebleCount) { feebleNum[i] = -1; }
            else
            {
                feebleNum[i] = player_feeble[i].index;
            }
        }
        return feebleNum;
    }

    private List<string> potentialWill = new List<string>();
    private List<string> potentialFeeble = new List<string>();

    private List<Quirk_Base> player_will = new List<Quirk_Base>();
    private List<Quirk_Base> player_feeble = new List<Quirk_Base>();

    private int willCount;
    private int feebleCount;

    public static event Action<int, Quirk_Base[]> WillChangedEvent;
    public static event Action<int, Quirk_Base[]> FeebleChangedEvent;

    private void OnEnable()
    {
        StatusManager.ObsessionMaxEvent += StatusManager_ObsessionMaxEvent;
    }

    private void OnDisable()
    {
        StatusManager.ObsessionMaxEvent -= StatusManager_ObsessionMaxEvent;
    }

    private void StatusManager_ObsessionMaxEvent()
    {
        int temp = UnityEngine.Random.Range(0, 2);
        if (temp == 0)
        {
            AddWill();
        }
        else
        {
            AddFeeble();
        }
        checkQuirks();
    }

    private void AddWill()
    {
        if (this.willCount == quirkLimit)
        {
            RemoveWill();
        }

        int temp = UnityEngine.Random.Range(0, potentialWill.Count);
        this.player_will.Add((Quirk_Base)Activator.CreateInstance(Type.GetType(potentialWill[temp])));
        potentialWill.RemoveAt(temp);
        this.willCount = this.player_will.Count;
        this.player_will[willCount - 1].onObtain();
    }

    private void AddWill(string name)
    {
        this.player_will.Add((Quirk_Base)Activator.CreateInstance(Type.GetType(name)));
        this.willCount = this.player_will.Count;
    }

    private void RemoveWill()
    {
        int temp = UnityEngine.Random.Range(0, willCount);
        if (willCount == 0) { return; }

        this.player_will[temp].onLose();
        potentialWill.Add(player_will[temp].realName);
        this.player_will.RemoveAt(temp);
        this.willCount = this.player_will.Count;
    }
    /*
    private void RemoveWill(int n)
    {
        if (willCount == 0) { return; }

        if (n > quirkLimit || n < 0) { return; }

        this.player_will[n].onLose();
        this.player_will.RemoveAt(n);
        this.willCount = this.player_will.Count;
    }
    */

    private void AddFeeble()
    {
        if (this.feebleCount == quirkLimit)
        {
            RemoveFeeble();
        }
        int temp = UnityEngine.Random.Range(0, potentialFeeble.Count);
        this.player_feeble.Add((Quirk_Base)Activator.CreateInstance(Type.GetType(potentialFeeble[temp])));
        potentialFeeble.RemoveAt(temp);
        this.feebleCount = this.player_feeble.Count;
        this.player_feeble[feebleCount - 1].onObtain();
    }

    private void AddFeeble(string name)
    {
        this.player_feeble.Add((Quirk_Base)Activator.CreateInstance(Type.GetType(name)));
        this.feebleCount = this.player_feeble.Count;
    }

    private void RemoveFeeble()
    {
        int temp = UnityEngine.Random.Range(0, feebleCount);
        if (feebleCount == 0) { return; }

        this.player_feeble[temp].onLose();
        potentialFeeble.Add(player_feeble[temp].realName);
        this.player_feeble.RemoveAt(temp);
        this.feebleCount = this.player_feeble.Count;
    }
    /*
    private void RemoveFeeble(int n)
    {
        if (feebleCount == 0) { return; }
        if (n > quirkLimit || n < 0) { return; }

        this.player_feeble[n].onLose();
        this.player_feeble.RemoveAt(n);
        this.feebleCount = this.player_feeble.Count;
    }
    */

    private void QuirkInitialize()
    {
        for(int i = 0; i < willCount; i++)
        {
            this.player_will[i].onObtain();
        }
        for(int j = 0; j < feebleCount; j++)
        {
            this.player_feeble[j].onObtain();
        }
    }


    private void checkQuirks()
    {
        this.willCount = this.player_will.Count;
        WillChangedEvent?.Invoke(willCount, player_will.ToArray());
        this.feebleCount = this.player_feeble.Count;
        FeebleChangedEvent?.Invoke(feebleCount, player_feeble.ToArray());
    }

    private void initQuirk(int[] initWill, int[] initFeeb)
    {
        int temp1 = initWill.Length;
        int temp2 = initFeeb.Length;
        for(int i = 0; i< temp1; i++)
        {
            if (initWill[i] != -1)
            {
                AddWill(potentialWill[initWill[i]]);
            }
        }
        for (int j = 0; j < temp2; j++)
        {
            if (initFeeb[j] != -1)
            {
                AddFeeble(potentialFeeble[initFeeb[j]]);
            }
        }
        checkQuirks();
    }

    private void initPotenQuirk(int[] initWill, int[] initFeeb)
    {
        int temp1 = initWill.Length;
        int temp2 = initFeeb.Length;
        for (int i = (temp1 - 1); i >= 0; i--)
        {
            if (initWill[i] != -1)
            {
                potentialWill.RemoveAt(initWill[i]);
            }
        }
        for (int j = (temp2 - 1); j >= 0; j--)
        {
            if (initFeeb[j] != -1)
            {
                potentialFeeble.RemoveAt(initFeeb[j]);
            }
        }
    }

    private void Awake()
    {
        int temp1 = willList.items.Count;
        int temp2 = feebleList.items.Count;
        for(int i = 0; i<temp1; i++)
        {
            this.potentialWill.Add(willList.items[i]);
        }
        for(int j = 0; j< temp2; j++)
        {
            this.potentialFeeble.Add(feebleList.items[j]);
        }

        int[] a = this.saveManager.saving.will;
        int[] b = this.saveManager.saving.feeble;
        initQuirk(a, b);

        Array.Sort(b);
        Array.Sort(a);
        initPotenQuirk(a, b);
    }

    private void Start()
    {
        QuirkInitialize();
        checkQuirks();
    }

}
