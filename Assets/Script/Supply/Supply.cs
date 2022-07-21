using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Supply_Base
{
    //�Ҹ�ǰ�� �̹����� realName���� ó��
    public string supplyName;
    public string realName;

    public string batDescription;
    public string description;
    //�� �� ���� �������°�?
    public bool isOnlyOnce;

    /*��� Ÿ�̹��� �����ΰ�?
    0 = ���� �ܿ����� | 1 = ���������� | 2 = ���� + ���� ��
    ���� ��� �⺮ ���ſ� �������� ���������� ����� �� ���⿡ 0��
    ���������� ������ �÷��ִ� �������� ���������� ����� �� �ֱ⿡ 1��
    ����ó�� ���¸� ȸ�������ִ� �������� ��𼭵� ����� �Ǳ� ������ 2��
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
        this.description = "�װ� �̰� ���� �ִٴ� ���� ������ ������ �ʾҴٴ� ���̰���... �׷�... �׷��ǰ�...";
        this.batDescription = "�װ� �̰� ���� �ִٴ� ���� �������ٴ� ���̰���... �� ���� �ִ°���? �Ѱ��� �� ����.";
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
        : base("�Ҹ�ǰ ��� �� ��", "Supply_NoUse", false, 1)
    {
        this.batDescription = "�Ҹ�ǰ�� ������� �ʽ��ϴ�.";
        this.description =
            "�̰� ����ü ��� �����ִ°���?";
    }
}

public class Supply_Opium : Supply_Base
{
    public Supply_Opium()
        : base("���� �� ��ġ", "Supply_Opium", true, 2)
    {
        this.batDescription = "�Ҹ�ǰ. ���⸦ 10��ŭ �����ϴ�.";
        this.description = "�Ҹ�ǰ. ���⸦ 10��ŭ �����ϴ�.";
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
        : base("������", "Supply_Painkiller", true, 1)
    {
        this.batDescription = "�Ҹ�ǰ. �̹� ��, ������ ���� ������ 2��ŭ ��½�ŵ�ϴ�.";
        this.description = "���������� ��� ����.\u000a" + "�Ҹ�ǰ. ����� ��, ������ ���� ������ 2��ŭ ��½�ŵ�ϴ�.";
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
        : base("���ܿ� �׸�", "Supply_Abhorrpainting", true, 0)
    {
        this.batDescription = "�����߿��� ����� �� �����ϴ�.";
        this.description = "�����߿��� ��� �Ұ�.\u000a" + "�Ҹ�ǰ. ����ϸ� ���� ������ ���������� 1����ϰ�, ��Ȥ ������ ���������� 1�����մϴ�.";
    }
    public override void onUse()
    {
        changeResist_eter(0, 0, 1, -1);
    }
}