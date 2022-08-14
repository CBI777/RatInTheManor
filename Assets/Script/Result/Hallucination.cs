using System;

public class Hallucination_Base
{
    public string halluName;
    public string realName;

    public int index;

    public string description;

    public static event Action<int> HalluMadnessChange;
    public static event Action<int> HalluSanityChange;
    public static event Action<int> HalluObsessionChange;
    public static event Action<int[]> HalluResistChange;
    public static event Action<int> HalluObtainWill;
    public static event Action<int> HalluObtainFeeble;
    public static event Action<int> HalluLoseWill;
    public static event Action<int> HalluLoseFeeble;
    public static event Action<int> HalluObtainObsession;
    public static event Action<int> HalluLoseObsession;

    public Hallucination_Base(string name, string rname, int index)
    {
        this.halluName = name;
        this.realName = rname;
        this.index = index;
    }

    public virtual void onUse() { }

    public void ChangeMadness(int n)
    {
        HalluMadnessChange?.Invoke(n);
    }
    public void ChangeSanity(int n)
    {
        HalluSanityChange?.Invoke(n);
    }
    public void ChangeObsession(int n)
    {
        HalluObsessionChange?.Invoke(n);
    }
    public void ChangeResist(int p, int f, int a, int d)
    {
        HalluResistChange?.Invoke(new int[4] { p, f, a, d });
    }
    public void ObtainWill(int n)
    {
        HalluObtainWill?.Invoke(n);
    }
    public void LoseWill(int n)
    {
        HalluLoseWill?.Invoke(n);
    }
    public void ObtainFeeble(int n)
    {
        HalluObtainFeeble?.Invoke(n);
    }
    public void LoseFeeble(int n)
    {
        HalluLoseFeeble?.Invoke(n);
    }
    public void ObtainObsession(int n)
    {
        HalluObtainObsession(n);
    }
    public void LoseObsession(int n)
    {
        HalluLoseObsession(n);
    }
}

public class Hallu_Fool : Hallucination_Base
{
    public Hallu_Fool()
        : base("���� ����ġ", "Hallu_Fool", 0)
    {
        this.description =
            "���� Ȥ�� ������ �� 2�� ȹ��.";
    }

    public override void onUse()
    {
        ObtainObsession(2);
    }
}

public class Hallu_Fool_Rev : Hallucination_Base
{
    public Hallu_Fool_Rev()
        : base("���� ����ġ", "Hallu_Fool_Rev", 1)
    {
        this.description =
            "���� Ȥ�� ������ �� 3�� ȹ��.";
    }
    public override void onUse()
    {
        ObtainObsession(3);
    }
}

public class Hallu_Magician : Hallucination_Base
{
    public Hallu_Magician()
        : base("������ ����ġ", "Hallu_Magician", 2)
    {
        this.description =
            "������ ������ �ϳ� ȹ��.";
    }
    public override void onUse()
    {
        ObtainWill(1);
    }
}

public class Hallu_Magician_Rev : Hallucination_Base
{
    public Hallu_Magician_Rev()
        : base("������ ����ġ", "Hallu_Magician_Rev", 3)
    {
        this.description =
            "������ ������ �ϳ� ȹ��.";
    }
    public override void onUse()
    {
        ObtainFeeble(1);
    }
}

public class Hallu_HighPries : Hallucination_Base
{
    public Hallu_HighPries()
        : base("����Ȳ ����ġ", "Hallu_HighPries", 4)
    {
        this.description =
            "�̼��� 30 ȸ��.";
    }
    public override void onUse()
    {
        ChangeSanity(-50);
    }
}

public class Hallu_HighPries_Rev : Hallucination_Base
{
    public Hallu_HighPries_Rev()
        : base("����Ȳ ����ġ", "Hallu_HighPries_Rev", 5)
    {
        this.description =
            "�̼��� 50 ����.";
    }
    public override void onUse()
    {
        ChangeSanity(50);
    }
}

public class Hallu_Empress : Hallucination_Base
{
    public Hallu_Empress()
        : base("���� ����ġ", "Hallu_Empress", 6)
    {
        this.description =
            "���� ������ 1 ����.";
    }
    public override void onUse()
    {
        ChangeResist(0, 1, 0, 0);
    }
}

public class Hallu_Empress_Rev : Hallucination_Base
{
    public Hallu_Empress_Rev()
        : base("���� ����ġ", "Hallu_Empress_Rev", 7)
    {
        this.description =
            "���� ������ 1 ����.";
    }
    public override void onUse()
    {
        ChangeResist(0, -1, 0, 0);
    }
}

public class Hallu_Emperor : Hallucination_Base
{
    public Hallu_Emperor()
        : base("Ȳ�� ����ġ", "Hallu_Emperor", 8)
    {
        this.description =
            "������ 50 ȸ��.";
    }
    public override void onUse()
    {
        ChangeObsession(-50);
    }
}

public class Hallu_Emperor_Rev : Hallucination_Base
{
    public Hallu_Emperor_Rev()
        : base("Ȳ�� ����ġ", "Hallu_Emperor_Rev", 9)
    {
        this.description =
            "������ 50 ȹ��.";
    }
    public override void onUse()
    {
        ChangeObsession(50);
    }
}

public class Hallu_Hiero : Hallucination_Base
{
    public Hallu_Hiero()
        : base("��Ȳ ����ġ", "Hallu_Hiero", 10)
    {
        this.description =
            "���� ������ 1 ����.";
    }
    public override void onUse()
    {
        ChangeResist(0, 0, 1, 0);
    }
}

public class Hallu_Hiero_Rev : Hallucination_Base
{
    public Hallu_Hiero_Rev()
        : base("��Ȳ ����ġ", "Hallu_Hiero_Rev", 11)
    {
        this.description =
            "���� ������ 1 ����.";
    }
    public override void onUse()
    {
        ChangeResist(0, 0, -1, 0);
    }
}

public class Hallu_Lovers : Hallucination_Base
{
    public Hallu_Lovers()
        : base("���� ����ġ", "Hallu_Lovers", 12)
    {
        this.description =
            "���⸦ 10 ȸ��.";
    }
    public override void onUse()
    {
        ChangeMadness(-10);
    }
}

public class Hallu_Lovers_Rev : Hallucination_Base
{
    public Hallu_Lovers_Rev()
        : base("���� ����ġ", "Hallu_Lovers_Rev", 13)
    {
        this.description =
            "���⸦ 10 ȹ��.";
    }
    public override void onUse()
    {
        ChangeMadness(10);
    }
}

public class Hallu_Chariot : Hallucination_Base
{
    public Hallu_Chariot()
        : base("���� ����ġ", "Hallu_Chariot", 14)
    {
        this.description =
            "���� ������ 1 ����.";
    }
    public override void onUse()
    {
        ChangeResist(0, 0, 0, 1);
    }
}

public class Hallu_Chariot_Rev : Hallucination_Base
{
    public Hallu_Chariot_Rev()
        : base("���� ����ġ", "Hallu_Chariot_Rev", 15)
    {
        this.description =
            "���� ������ 1 ����.";
    }
    public override void onUse()
    {
        ChangeResist(0, 0, 0, -1);
    }
}

public class Hallu_Strength : Hallucination_Base
{
    public Hallu_Strength()
        : base("�� ����ġ", "Hallu_Strength", 16)
    {
        this.description =
            "������ ���� ������ 1 ����.";
    }
    public override void onUse()
    {
        ChangeResist(1, 0, 0, 0);
    }
}

public class Hallu_Strength_Rev : Hallucination_Base
{
    public Hallu_Strength_Rev()
        : base("�� ����ġ", "Hallu_Strength_Rev", 17)
    {
        this.description =
            "������ ���� ������ 1 ����.";
    }
    public override void onUse()
    {
        ChangeResist(-1, 0, 0, 0);
    }
}

public class Hallu_Hermit : Hallucination_Base
{
    public Hallu_Hermit()
        : base("������ ����ġ", "Hallu_Hermit", 18)
    {
        this.description =
            "������ ������ �ϳ� ����.";
    }
    public override void onUse()
    {
        LoseFeeble(1);
    }
}

public class Hallu_Hermit_Rev : Hallucination_Base
{
    public Hallu_Hermit_Rev()
        : base("������ ����ġ", "Hallu_Hermit_Rev", 19)
    {
        this.description =
            "������ ������ �ϳ� ����.";
    }
    public override void onUse()
    {
        LoseWill(1);
    }
}