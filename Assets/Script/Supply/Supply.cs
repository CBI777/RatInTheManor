using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Supply_Base
{
    //소모품의 이미지는 realName으로 처리
    public string supplyName;
    public string realName;

    public string batDescription;
    public string description;
    //한 번 쓰면 없어지는가?
    public bool isOnlyOnce;

    /*사용 타이밍은 언제인가?
    0 = 전투 외에서만 | 1 = 전투에서만 | 2 = 전투 + 전투 외
    예를 들어 기벽 제거용 아이템은 전투에서는 사용할 수 없기에 0번
    순간적으로 내성을 올려주는 아이템은 전투에서만 사용할 수 있기에 1번
    아편처럼 상태를 회복시켜주는 아이템은 어디서든 사용이 되기 때문에 2번
    */
    public int usage;

    public static event Action<int[]> SupplyResistChangeTemp;
    public static event Action<int[]> SupplyResistChangeEter;
    public static event Action<int> SupplyMadnessChange;
    public static event Action<int> SupplySanityChange;
    public static event Action<int> SupplyObsessionChange;
    //public static event Action SupplyAilmentChange;

    public Supply_Base(string name, string rname, bool onceornot, int usage)
    {
        this.supplyName = name;
        this.realName = rname;
        this.isOnlyOnce = onceornot;
        this.usage = usage;
        this.description = "네가 이걸 보고 있다는 것은 설명이 적히지 않았다는 뜻이겠지... 그래... 그런건가...";
        this.batDescription = "네가 이걸 보고 있다는 것은 게을렀다는 뜻이겠지... 뭘 보고 있는거지? 한가한 것 같군.";
    }
    public virtual void onObtain() { }
    public virtual void onUse() { }
    public virtual void onStopUse() { }
    public virtual void onLose() { }

    public void changeMadness(int n)
    {
        SupplyMadnessChange?.Invoke(n);
    }
    public void changeSanity(int n)
    {
        SupplySanityChange?.Invoke(n);
    }
    public void changeObsession(int n)
    {
        SupplyObsessionChange?.Invoke(n);
    }
    public void changeResist_temp(int p, int f, int a, int d)
    {
        SupplyResistChangeTemp?.Invoke(new int[4] { p, f, a, d });
    }
    public void changeResist_eter(int p, int f, int a, int d)
    {
        SupplyResistChangeEter?.Invoke(new int[4] { p, f, a, d });
    }
}

public class Supply_NoUse : Supply_Base
{
    public Supply_NoUse()
        : base("소모품 사용 안 함", "Supply_NoUse", false, 1)
    {
        this.batDescription = "소모품을 사용하지 않습니다.";
        this.description =
            "이걸 도대체 어떻게 보고있는거지?";
    }
}

public class Supply_Opium : Supply_Base
{
    public Supply_Opium()
        : base("아편 한 뭉치", "Supply_Opium", true, 2)
    {
        this.batDescription = "소모품. 광기를 10만큼 잠재웁니다.";
        this.description = "소모품. 광기를 10만큼 잠재웁니다.";
    }

    public override void onUse()
    {
        changeMadness(-10);
    }
    public override void onStopUse()
    {
        changeMadness(10);
    }
}

public class Supply_Painkiller : Supply_Base
{
    public Supply_Painkiller()
        : base("진통제", "Supply_Painkiller", true, 1)
    {
        this.batDescription = "소모품. 이번 턴, 물리적 피해 저항을 2만큼 상승시킵니다.";
        this.description = "전투에서만 사용 가능.\u000a" + "소모품. 사용한 턴, 물리적 피해 저항을 2만큼 상승시킵니다.";
    }

    public override void onUse()
    {
        changeResist_temp(2, 0, 0, 0);
    }
    public override void onStopUse()
    {
        changeResist_temp(-2, 0, 0, 0);
    }
}


public class Supply_Abhorrpainting : Supply_Base
{
    public Supply_Abhorrpainting()
        : base("역겨운 그림", "Supply_Abhorrpainting", true, 0)
    {
        this.batDescription = "전투중에는 사용할 수 없습니다.";
        this.description = "전투중에는 사용 불가.\u000a" + "소모품. 사용하면 혐오 저항이 영구적으로 1상승하고, 현혹 저항이 영구적으로 1감소합니다.";
    }
    public override void onUse()
    {
        changeResist_eter(0, 0, 1, -1);
    }
}