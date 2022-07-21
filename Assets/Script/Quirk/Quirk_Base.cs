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

    public static event Action<int[]> QuirkResistChangeEvent;

    public Quirk_Base(string quirkName, string realName,  bool isWill)
    {
        this.quirkName = quirkName;
        this.realName = realName;
        this.isWill = isWill;
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
}

public class Quirk_BasicUpPhys : Quirk_Base
{
    public Quirk_BasicUpPhys()
        : base("튼튼한 육체", "Quirk_BasicUpPhys", true)
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
        : base("강건한 마음", "Quirk_BasicUpFear", true)
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
        : base("외면", "Quirk_BasicUpAbhorr", true)
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
        : base("참을성", "Quirk_BasicUpDelus", true)
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

public class Quirk_BasicDownPhys : Quirk_Base
{
    public Quirk_BasicDownPhys()
        : base("연약", "Quirk_BasicDownPhys", false)
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
        : base("피해 망상", "Quirk_BasicDownFear", false)
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
        : base("약한 비위", "Quirk_BasicDownAbhorr", false)
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
        : base("호기심", "Quirk_BasicDownDelus", false)
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