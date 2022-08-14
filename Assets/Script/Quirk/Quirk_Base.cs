using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Quirk_Base
{
    public string quirkName;
    public string realName;
    public string quirkDescription = "그래... 이곳에 도달했군... 그러나 아직 때가 아니다.";
    //true면 의지 will, false면 나약 feebleness
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
        : base("튼튼한 육체", "Quirk_BasicUpPhys", true, 0)
    {
        this.quirkDescription = "물리적 피해 저항 + 1";
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
        : base("강건한 마음", "Quirk_BasicUpFear", true, 1)
    {
        this.quirkDescription = "공포 저항 + 1";
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
        : base("외면", "Quirk_BasicUpAbhorr", true, 2)
    {
        this.quirkDescription = "혐오 저항 + 1";
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
        : base("참을성", "Quirk_BasicUpDelus", true, 3)
    {
        this.quirkDescription = "현혹 저항 + 1";
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
        : base("신앙심", "Quirk_TokenUpOne", false, 4)
    {
        this.quirkDescription = "매 턴 방어 토큰을 한 개 더 얻음";
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
        : base("용감무쌍", "Quirk_Phys2Abhorr1", true, 5)
    {
        this.quirkDescription = "물리적 피해 저항 + 2\u000a혐오 저항 - 1";
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
        : base("산전수전", "Quirk_Fear2Delus1", true, 6)
    {
        this.quirkDescription = "공포 저항 + 2\u000a현혹 저항 - 1";
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
        : base("연약", "Quirk_BasicDownPhys", false, 0)
    {
        this.quirkDescription = "물리적 피해 저항 - 1";
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
        : base("피해 망상", "Quirk_BasicDownFear", false, 1)
    {
        this.quirkDescription = "공포 저항 - 1";
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
        : base("약한 비위", "Quirk_BasicDownAbhorr", false, 2)
    {
        this.quirkDescription = "혐오 저항 - 1";
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
        : base("호기심", "Quirk_BasicDownDelus", false, 3)
    {
        this.quirkDescription = "현혹 저항 - 1";
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
        : base("엉성한 움직임", "Quirk_TokenDownOne", false, 4)
    {
        this.quirkDescription = "매 턴 방어 토큰을 한 개 적게 얻음";
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
        : base("체념", "Quirk_AllDownOne", false, 5)
    {
        this.quirkDescription = "모든 저항 - 1";
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
        : base("주의 산만", "Quirk_SevereDownDelus", false, 6)
    {
        this.quirkDescription = "현혹 저항 - 3";
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