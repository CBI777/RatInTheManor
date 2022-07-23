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

public class Equipment_Pistol : Equipment
{
    public Equipment_Pistol()
        : base("피스톨", "Equipment_Pistol", 0, 1, -1, 0)
    {
        this.description = "공포 저항 + 1\u000a혐오 저항 - 1";
    }
}

public class Equipment_Lantern : Equipment
{
    public Equipment_Lantern() 
        : base("기묘한 등불", "Equipment_Lantern", 0, -1, 0, 1)
    {
        this.description = "공포 저항 - 1\u000a현혹 저항 + 1";
    }
}

public class Equipment_EyeballJar : Equipment
{
    public Equipment_EyeballJar()
        : base("눈알이 든 병", "Equipment_EyeballJar", 3, -1, -1, -1)
    {
        this.description = "물리적 저항 + 3\u000a공포 저항 -1\u000a혐오 저항 - 1\u000a현혹 저항 -1\u000a";
    }
}

public class Equipment_Blindfold : Equipment
{
    public Equipment_Blindfold()
        : base("눈 가리개", "Equipment_Blindfold", -4, 1, 1, 1)
    {
        this.description = "물리적 저항 - 4\u000a공포 저항 + 1\u000a혐오 저항 + 1\u000a현혹 저항 + 1\u000a";
    }
}
