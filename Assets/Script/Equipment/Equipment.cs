using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    //장비의 이미지는 realName으로 처리
    public string equipName;
    public string realName;
    public string description;
    public int[] resChange = new int[4];

    public Equipment(string name, string realname, int p, int f, int a, int d)
    {
        this.equipName = name;
        this.realName = realname;
        this.resChange[0] = p;
        this.resChange[1] = f;
        this.resChange[2] = a;
        this.resChange[3] = d;
        this.description = "네가 이걸 보고 있다는 것은 설명이 적히지 않았다는 뜻이겠지... 그래... 그런건가...";
    }
}

public class Equipment_TempEquip1 : Equipment
{
    public Equipment_TempEquip1()
        : base("임시 장비1", "Equipment_TempEquip1", 1, 1, 0, -1)
    {
        this.description = "임시로 주어진 장비. 이걸로 어쩌라고?";
    }
}

public class Equipment_TempEquip2 : Equipment
{
    public Equipment_TempEquip2() 
        : base("임시 장비2", "Equipment_TempEquip2", 0, 1, 0, -1)
    {
        this.description = "임시로 주어진 장비. 이건 정말이지 엉망이군.";
    }
}

public class Equipment_TempEquip3 : Equipment
{
    public Equipment_TempEquip3()
        : base("임시 장비3", "Equipment_TempEquip3", 3, -1, -1, -1)
    {
        this.description = "임시로 주어진 장비. 때로는 공격이 최선의 선택이 된다.\u000a - 이놈은 뭐라는거야?";
    }
}
