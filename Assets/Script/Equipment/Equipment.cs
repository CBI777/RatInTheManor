using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    //장비의 이미지는 realName으로 처리
    public string equipName;
    public string realName;
    public string description = "네가 이걸 보고 있다는 것은 설명이 적히지 않았다는 뜻이겠지... 그래... 그런건가...";
    public int[] resChange = new int[4];

    public Equipment(string name, string realname, int p, int f, int a, int d)
    {
        this.equipName = name;
        this.realName = realname;
        this.resChange[0] = p;
        this.resChange[1] = f;
        this.resChange[2] = a;
        this.resChange[3] = d;
    }
}

public class Equipment_TempEquip1 : Equipment
{
    public Equipment_TempEquip1()
        : base("임시 장비1", "Equipment_TempEquip1", 1, 1, 0, -1)
    {
        this.description = "물리적 저항 + 1\u000a공포 저항 + 1\u000a현혹 저항 - 1";
    }
}

public class Equipment_TempEquip2 : Equipment
{
    public Equipment_TempEquip2() 
        : base("임시 장비2", "Equipment_TempEquip2", 0, 1, 0, -1)
    {
        this.description = "공포 저항 + 1\u000a현혹 저항 - 1";
    }
}

public class Equipment_TempEquip3 : Equipment
{
    public Equipment_TempEquip3()
        : base("임시 장비3", "Equipment_TempEquip3", 3, -1, -1, -1)
    {
        this.description = "물리적 저항 + 3\u000a공포 저항 -1\u000a혐오 저항 - 1\u000a현혹 저항 -1\u000a";
    }
}
