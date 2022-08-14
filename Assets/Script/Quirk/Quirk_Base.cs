using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Quirk_Base
{
    public string quirkName;
    public string realName;
    public string quirkDescription = "�׷�... �̰��� �����߱�... �׷��� ���� ���� �ƴϴ�.";
    //true�� ���� will, false�� ���� feebleness
    public bool isWill;
    public int index;

    public static event Action<int[]> QuirkResistChangeEvent;
    public static event Action<int> TokenChangeEvent;

    public Quirk_Base(string quirkName, string realName,  bool isWill, int index)
    {
        this.quirkName = quirkName;
        this.realName = realName;
        this.isWill = isWill;
        this.index = index;
    }

    public virtual void onObtain() { }
    public virtual void onBattleStart() { }
    public virtual void onTurnStart() { }
    public virtual void onTurnEnd() { }
    public virtual void onBattleEnd() { }
    public virtual void onLose() { }

    public void changeResist(int p, int f, int a, int d)
    {
        QuirkResistChangeEvent?.Invoke(new int[4] { p, f, a, d });
    }

    public void changeToken(int n)
    {
        TokenChangeEvent?.Invoke(n);
    }
}

public class Quirk_BasicUpPhys : Quirk_Base
{
    public Quirk_BasicUpPhys()
        : base("ưư�� ��ü", "Quirk_BasicUpPhys", true, 0)
    {
        this.quirkDescription = "������ ���� ���� + 1";
    }

    public override void onObtain()
    {
        changeResist(1, 0, 0, 0);
    }

    public override void onLose()
    {
        changeResist(-1, 0, 0, 0);
    }
}

public class Quirk_BasicUpFear : Quirk_Base
{
    public Quirk_BasicUpFear()
        : base("������ ����", "Quirk_BasicUpFear", true, 1)
    {
        this.quirkDescription = "���� ���� + 1";
    }

    public override void onObtain()
    {
        changeResist(0, 1, 0, 0);
    }

    public override void onLose()
    {
        changeResist(0, -1, 0, 0);
    }
}

public class Quirk_BasicUpAbhorr : Quirk_Base
{
    public Quirk_BasicUpAbhorr()
        : base("�ܸ�", "Quirk_BasicUpAbhorr", true, 2)
    {
        this.quirkDescription = "���� ���� + 1";
    }

    public override void onObtain()
    {
        
        changeResist(0, 0, 1, 0);
    }

    public override void onLose()
    {
        changeResist(0, 0, -1, 0);
    }
}

public class Quirk_BasicUpDelus : Quirk_Base
{
    public Quirk_BasicUpDelus()
        : base("������", "Quirk_BasicUpDelus", true, 3)
    {
        this.quirkDescription = "��Ȥ ���� + 1";
    }

    public override void onObtain()
    {
        changeResist(0, 0, 0, 1);
    }

    public override void onLose()
    {
        changeResist(0, 0, 0, -1);
    }
}

public class Quirk_TokenUpOne : Quirk_Base
{
    public Quirk_TokenUpOne()
        : base("�žӽ�", "Quirk_TokenUpOne", false, 4)
    {
        this.quirkDescription = "�� �� ��� ��ū�� �� �� �� ����";
    }

    public override void onObtain()
    {
        changeToken(1);
    }
    public override void onLose()
    {
        changeToken(-1);
    }
}

public class Quirk_Phys2Abhorr1 : Quirk_Base
{
    public Quirk_Phys2Abhorr1()
        : base("�밨����", "Quirk_Phys2Abhorr1", true, 5)
    {
        this.quirkDescription = "������ ���� ���� + 2\u000a���� ���� - 1";
    }

    public override void onObtain()
    {
        changeResist(2, 0, -1, 0);
    }

    public override void onLose()
    {
        changeResist(-2, 0, 1, 0);
    }
}

public class Quirk_Fear2Delus1 : Quirk_Base
{
    public Quirk_Fear2Delus1()
        : base("��������", "Quirk_Fear2Delus1", true, 6)
    {
        this.quirkDescription = "���� ���� + 2\u000a��Ȥ ���� - 1";
    }

    public override void onObtain()
    {
        changeResist(0, 2, 0, -1);
    }

    public override void onLose()
    {
        changeResist(0, 2, 0, 1);
    }
}


/////////////////////////////////////////////////////////////////////////////


public class Quirk_BasicDownPhys : Quirk_Base
{
    public Quirk_BasicDownPhys()
        : base("����", "Quirk_BasicDownPhys", false, 0)
    {
        this.quirkDescription = "������ ���� ���� - 1";
    }

    public override void onObtain()
    {
        changeResist(-1, 0, 0, 0);
    }

    public override void onLose()
    {
        changeResist(1, 0, 0, 0);
    }
}

public class Quirk_BasicDownFear : Quirk_Base
{
    public Quirk_BasicDownFear()
        : base("���� ����", "Quirk_BasicDownFear", false, 1)
    {
        this.quirkDescription = "���� ���� - 1";
    }

    public override void onObtain()
    {
        changeResist(0, -1, 0, 0);
    }

    public override void onLose()
    {
        changeResist(0, 1, 0, 0);
    }
}

public class Quirk_BasicDownAbhorr : Quirk_Base
{
    public Quirk_BasicDownAbhorr()
        : base("���� ����", "Quirk_BasicDownAbhorr", false, 2)
    {
        this.quirkDescription = "���� ���� - 1";
    }

    public override void onObtain()
    {
        changeResist(0, 0, -1, 0);
    }

    public override void onLose()
    {
        changeResist(0, 0, 1, 0);
    }
}

public class Quirk_BasicDownDelus : Quirk_Base
{
    public Quirk_BasicDownDelus()
        : base("ȣ���", "Quirk_BasicDownDelus", false, 3)
    {
        this.quirkDescription = "��Ȥ ���� - 1";
    }

    public override void onObtain()
    {
        changeResist(0, 0, 0, -1);
    }

    public override void onLose()
    {
        changeResist(0, 0, 0, 1);
    }
}

public class Quirk_TokenDownOne : Quirk_Base
{
    public Quirk_TokenDownOne()
        : base("������ ������", "Quirk_TokenDownOne", false, 4)
    {
        this.quirkDescription = "�� �� ��� ��ū�� �� �� ���� ����";
    }

    public override void onObtain()
    {
        changeToken(-1);
    }
    public override void onLose()
    {
        changeToken(1);
    }
}

public class Quirk_AllDownOne : Quirk_Base
{
    public Quirk_AllDownOne()
        : base("ü��", "Quirk_AllDownOne", false, 5)
    {
        this.quirkDescription = "��� ���� - 1";
    }

    public override void onObtain()
    {
        changeResist(-1, -1, -1, -1);
    }

    public override void onLose()
    {
        changeResist(1, 1, 1, 1);
    }
}

public class Quirk_SevereDownDelus : Quirk_Base
{
    public Quirk_SevereDownDelus()
        : base("���� �길", "Quirk_SevereDownDelus", false, 6)
    {
        this.quirkDescription = "��Ȥ ���� - 3";
    }

    public override void onObtain()
    {
        changeResist(0, 0, 0, -3);
    }

    public override void onLose()
    {
        changeResist(0, 0, 0, 3);
    }
}