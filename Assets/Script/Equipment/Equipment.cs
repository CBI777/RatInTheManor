using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "ScriptableObject/Equipment")]
public class Equipment : ScriptableObject
{
    //장비의 이미지는 realName으로 처리
    public string equipName;
    public string realName;
    [TextArea] public string description;
    public int[] resChange = new int[4];
}