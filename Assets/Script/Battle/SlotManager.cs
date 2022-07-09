using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DmgType
{
    Phys, Fear, Abhorr, Delus
};

public class SlotManager : MonoBehaviour
{
    public static event Action<float, float, float, float> CoefChanged;
    public static event Action<int[]> TotalDmgChanged;
    public static event Action<int, int, int, int, int> FinalDmgChanged;

    [SerializeField] private int curSkill = 0;

    private List<EnemySkill> skillList = new List<EnemySkill>();

    private int[] totalDmg = new int[5];
    private int[,] finalDmg = new int[3, 5];
    private int[] resistance = new int[4];
    private int[] playerSlot = new int[4];
    private float[,] coef = new float[3, 4];

    private void OnEnable()
    {
        NormalTokenSlot.PlayerSlotChangedEvent += NormalTokenSlot_playerSlotChangedEvent;
        SkillManager.SkillAddedEvent += SkillManager_SkillAddedEvent;
        SkillBtn.SkillChanged += SkillBtn_SkillChanged;
        Player.ResistChangedEvent += Player_ResistChangedEvent;
    }

    private void OnDisable()
    {
        NormalTokenSlot.PlayerSlotChangedEvent -= NormalTokenSlot_playerSlotChangedEvent;
        SkillManager.SkillAddedEvent -= SkillManager_SkillAddedEvent;
        SkillBtn.SkillChanged -= SkillBtn_SkillChanged;
        Player.ResistChangedEvent -= Player_ResistChangedEvent;
    }


    private void Player_ResistChangedEvent(int[] obj)
    {
        for(int i =0; i < obj.Length; i++)
        {
            resistance[i] = obj[i];
        }
        reCalcAll();
    }
    private void NormalTokenSlot_playerSlotChangedEvent(DmgType arg1, int arg2)
    {
        playerSlot[(int)arg1] = arg2;
        reCalcDamage(((int)arg1));
    }
    private void SkillManager_SkillAddedEvent(List<EnemySkill> arg1, int arg2)
    {
        for(int i = 0; i< arg2;i++)
        {
            skillList.Add(arg1[i]);
        }
        reCalcAll();
    }
    private void SkillBtn_SkillChanged(EnemySkill arg1, int arg2)
    {
        curSkill = arg2;
        CoefChanged?.Invoke(coef[curSkill, 0], coef[curSkill, 1], coef[curSkill, 2], coef[curSkill, 3]);
        FinalDmgChanged?.Invoke(finalDmg[curSkill, 0], finalDmg[curSkill, 1], finalDmg[curSkill, 2], finalDmg[curSkill, 3], finalDmg[curSkill, 4]);
    }

    //enum에서의 int임.
    private void calcCoef(int arg1)
    {
        for(int i = 0; i < skillList.Count; i++)
        {
            int temp = (resistance[arg1] + playerSlot[arg1] - skillList[i].tokens[arg1]);
            if(temp <= -4) { coef[i, arg1] = 3.0f; }
            else if(temp == -3) { coef[i, arg1] = 2.0f; }
            else if(temp == -2) { coef[i, arg1] = 1.5f; }
            else if (temp == -1) { coef[i, arg1] = 1.25f; }
            else if (temp == 0) { coef[i, arg1] = 1.0f; }
            else if (temp == 1) { coef[i, arg1] = 0.75f; }
            else if (temp == 2) { coef[i, arg1] = 0.5f; }
            else if (temp == 3) { coef[i, arg1] = 0.25f; }
            else { coef[i, arg1] = 0; }
        }
    }

    private void calcDmgSum()
    {
        for(int i = 0; i<5; i++)
        {
            totalDmg[i] = 0;
        }
        //합산하고 전체합산을 알려줘야됨
        for (int i = 0; i < skillList.Count; i++)
        {
            finalDmg[i, 4] = 0;
            for (int j = 0; j < 4; j++)
            {
                totalDmg[j] += finalDmg[i, j];
                finalDmg[i, 4] += finalDmg[i, j];
            }
            totalDmg[4] += finalDmg[i, 4];
        }
    }

    //enum에서의 int임.
    private void reCalcDamage(int arg1)
    {
        calcCoef(arg1);
        for (int i = 0; i < skillList.Count; i++)
        {
            finalDmg[i, arg1] = (int)Mathf.Ceil(skillList[i].dmgs[arg1] * coef[i, arg1]);
        }

        calcDmgSum();

        CoefChanged?.Invoke(coef[curSkill, 0], coef[curSkill, 1], coef[curSkill, 2], coef[curSkill, 3]);
        FinalDmgChanged?.Invoke(finalDmg[curSkill, 0], finalDmg[curSkill, 1], finalDmg[curSkill, 2], finalDmg[curSkill, 3], finalDmg[curSkill, 4]);
        TotalDmgChanged?.Invoke(this.totalDmg);
    }

    private void reCalcAll()
    {
        for (int i = 0; i < skillList.Count; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                calcCoef(j);
                finalDmg[i, j] = (int)Mathf.Ceil(skillList[i].dmgs[j] * coef[i, j]);
            }
        }

        calcDmgSum();

        CoefChanged?.Invoke(coef[curSkill, 0], coef[curSkill, 1], coef[curSkill, 2], coef[curSkill, 3]);
        FinalDmgChanged?.Invoke(finalDmg[curSkill, 0], finalDmg[curSkill, 1], finalDmg[curSkill, 2], finalDmg[curSkill, 3], finalDmg[curSkill, 4]);
        TotalDmgChanged?.Invoke(this.totalDmg);
    }
}
