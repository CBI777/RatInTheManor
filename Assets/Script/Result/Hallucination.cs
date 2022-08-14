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
        : base("광대 정위치", "Hallu_Fool", 0)
    {
        this.description =
            "의지 혹은 나약을 총 2개 획득.";
    }

    public override void onUse()
    {
        ObtainObsession(2);
    }
}

public class Hallu_Fool_Rev : Hallucination_Base
{
    public Hallu_Fool_Rev()
        : base("광대 역위치", "Hallu_Fool_Rev", 1)
    {
        this.description =
            "의지 혹은 나약을 총 3개 획득.";
    }
    public override void onUse()
    {
        ObtainObsession(3);
    }
}

public class Hallu_Magician : Hallucination_Base
{
    public Hallu_Magician()
        : base("마법사 정위치", "Hallu_Magician", 2)
    {
        this.description =
            "무작위 의지를 하나 획득.";
    }
    public override void onUse()
    {
        ObtainWill(1);
    }
}

public class Hallu_Magician_Rev : Hallucination_Base
{
    public Hallu_Magician_Rev()
        : base("마법사 역위치", "Hallu_Magician_Rev", 3)
    {
        this.description =
            "무작위 나약을 하나 획득.";
    }
    public override void onUse()
    {
        ObtainFeeble(1);
    }
}

public class Hallu_HighPries : Hallucination_Base
{
    public Hallu_HighPries()
        : base("여법황 정위치", "Hallu_HighPries", 4)
    {
        this.description =
            "이성을 30 회복.";
    }
    public override void onUse()
    {
        ChangeSanity(-50);
    }
}

public class Hallu_HighPries_Rev : Hallucination_Base
{
    public Hallu_HighPries_Rev()
        : base("여법황 역위치", "Hallu_HighPries_Rev", 5)
    {
        this.description =
            "이성을 50 감소.";
    }
    public override void onUse()
    {
        ChangeSanity(50);
    }
}

public class Hallu_Empress : Hallucination_Base
{
    public Hallu_Empress()
        : base("여제 정위치", "Hallu_Empress", 6)
    {
        this.description =
            "공포 저항이 1 증가.";
    }
    public override void onUse()
    {
        ChangeResist(0, 1, 0, 0);
    }
}

public class Hallu_Empress_Rev : Hallucination_Base
{
    public Hallu_Empress_Rev()
        : base("여제 역위치", "Hallu_Empress_Rev", 7)
    {
        this.description =
            "공포 저항이 1 감소.";
    }
    public override void onUse()
    {
        ChangeResist(0, -1, 0, 0);
    }
}

public class Hallu_Emperor : Hallucination_Base
{
    public Hallu_Emperor()
        : base("황제 정위치", "Hallu_Emperor", 8)
    {
        this.description =
            "집착을 50 회복.";
    }
    public override void onUse()
    {
        ChangeObsession(-50);
    }
}

public class Hallu_Emperor_Rev : Hallucination_Base
{
    public Hallu_Emperor_Rev()
        : base("황제 역위치", "Hallu_Emperor_Rev", 9)
    {
        this.description =
            "집착을 50 획득.";
    }
    public override void onUse()
    {
        ChangeObsession(50);
    }
}

public class Hallu_Hiero : Hallucination_Base
{
    public Hallu_Hiero()
        : base("법황 정위치", "Hallu_Hiero", 10)
    {
        this.description =
            "혐오 저항이 1 증가.";
    }
    public override void onUse()
    {
        ChangeResist(0, 0, 1, 0);
    }
}

public class Hallu_Hiero_Rev : Hallucination_Base
{
    public Hallu_Hiero_Rev()
        : base("법황 역위치", "Hallu_Hiero_Rev", 11)
    {
        this.description =
            "혐오 저항이 1 감소.";
    }
    public override void onUse()
    {
        ChangeResist(0, 0, -1, 0);
    }
}

public class Hallu_Lovers : Hallucination_Base
{
    public Hallu_Lovers()
        : base("연인 정위치", "Hallu_Lovers", 12)
    {
        this.description =
            "광기를 10 회복.";
    }
    public override void onUse()
    {
        ChangeMadness(-10);
    }
}

public class Hallu_Lovers_Rev : Hallucination_Base
{
    public Hallu_Lovers_Rev()
        : base("연인 역위치", "Hallu_Lovers_Rev", 13)
    {
        this.description =
            "광기를 10 획득.";
    }
    public override void onUse()
    {
        ChangeMadness(10);
    }
}

public class Hallu_Chariot : Hallucination_Base
{
    public Hallu_Chariot()
        : base("전차 정위치", "Hallu_Chariot", 14)
    {
        this.description =
            "혐오 저항이 1 증가.";
    }
    public override void onUse()
    {
        ChangeResist(0, 0, 0, 1);
    }
}

public class Hallu_Chariot_Rev : Hallucination_Base
{
    public Hallu_Chariot_Rev()
        : base("전차 역위치", "Hallu_Chariot_Rev", 15)
    {
        this.description =
            "혐오 저항이 1 감소.";
    }
    public override void onUse()
    {
        ChangeResist(0, 0, 0, -1);
    }
}

public class Hallu_Strength : Hallucination_Base
{
    public Hallu_Strength()
        : base("힘 정위치", "Hallu_Strength", 16)
    {
        this.description =
            "물리적 피해 저항이 1 증가.";
    }
    public override void onUse()
    {
        ChangeResist(1, 0, 0, 0);
    }
}

public class Hallu_Strength_Rev : Hallucination_Base
{
    public Hallu_Strength_Rev()
        : base("힘 역위치", "Hallu_Strength_Rev", 17)
    {
        this.description =
            "물리적 피해 저항이 1 감소.";
    }
    public override void onUse()
    {
        ChangeResist(-1, 0, 0, 0);
    }
}

public class Hallu_Hermit : Hallucination_Base
{
    public Hallu_Hermit()
        : base("은둔자 정위치", "Hallu_Hermit", 18)
    {
        this.description =
            "무작위 나약을 하나 제거.";
    }
    public override void onUse()
    {
        LoseFeeble(1);
    }
}

public class Hallu_Hermit_Rev : Hallucination_Base
{
    public Hallu_Hermit_Rev()
        : base("은둔자 역위치", "Hallu_Hermit_Rev", 19)
    {
        this.description =
            "무작위 의지를 하나 제거.";
    }
    public override void onUse()
    {
        LoseWill(1);
    }
}