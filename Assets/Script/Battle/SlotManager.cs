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
    public static event Action<int> TotalDmgChanged;
    public static event Action<int, int, int, int, int> FinalDmgChanged;

    [SerializeField] private List<EnemySkill> skillList = new List<EnemySkill>();

    [SerializeField] private int totalDmg = 0;
    [SerializeField] private int curSkill = 0;

    private int[,] finalDmg = new int[3, 5];
    private int[] resistance = new int[4];
    private int[] playerSlot = new int[4];
    private float[,] coef = new float[3, 4];

    private void OnEnable()
    {
        NormalTokenSlot.PlayerSlotChangedEvent += NormalTokenSlot_playerSlotChangedEvent;
        SkillManager.SkillAddedEvent += SkillManager_SkillAddedEvent;
        SkillBtn.SkillChanged += SkillBtn_SkillChanged;
    }

    private void OnDisable()
    {
        NormalTokenSlot.PlayerSlotChangedEvent -= NormalTokenSlot_playerSlotChangedEvent;
        SkillManager.SkillAddedEvent -= SkillManager_SkillAddedEvent;
    }


    private void NormalTokenSlot_playerSlotChangedEvent(DmgType arg1, int arg2)
    {
        playerSlot[(int)arg1] = arg2;
        reCalcDamage(((int)arg1));
    }
    private void SkillManager_SkillAddedEvent(List<EnemySkill> arg1, int arg2)
    {
        startSlotTurn();

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

    //enum에서의 int임.
    private void reCalcDamage(int arg1)
    {
        calcCoef(arg1);
        for (int i = 0; i < skillList.Count; i++)
        {
            finalDmg[i, arg1] = (int)(skillList[i].dmgs[arg1] * coef[i, arg1]);
        }

        totalDmg = 0;
        //합산하고 전체합산을 알려줘야됨
        for(int i = 0; i < skillList.Count; i++)
        {
            finalDmg[i, 4] = 0;
            for(int j = 0; j<4; j++)
            {
                finalDmg[i, 4] += finalDmg[i, j];
            }
            totalDmg += finalDmg[i, 4];
        }
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
        for (int i = 0; i < skillList.Count; i++)
        {
            finalDmg[i, 4] = 0;
            for (int j = 0; j < 4; j++)
            {
                finalDmg[i, 4] += finalDmg[i, j];
            }
            totalDmg += finalDmg[i, 4];
        }
        CoefChanged?.Invoke(coef[curSkill, 0], coef[curSkill, 1], coef[curSkill, 2], coef[curSkill, 3]);
        FinalDmgChanged?.Invoke(finalDmg[curSkill, 0], finalDmg[curSkill, 1], finalDmg[curSkill, 2], finalDmg[curSkill, 3], finalDmg[curSkill, 4]);
        TotalDmgChanged?.Invoke(this.totalDmg);
    }

    public void startSlotTurn()
    {
        totalDmg = 0;
        curSkill = 0;
        skillList.Clear();
        for (int i = 0; i < 4; i++)
        {
            resistance[i] = 0;
            playerSlot[i] = 0;
        }
        for (int i = 0; i<3; i++)
        {
            for(int j = 0; j<4; j++)
            {
                finalDmg[i, j] = 0;
                coef[i, j] = 0;
            }
            finalDmg[i, 4] = 0;
        }
    }
}
