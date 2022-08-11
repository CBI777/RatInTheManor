using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "ScriptableObject/Equipment")]
public class Equipment : ScriptableObject
{
    //����� �̹����� realName���� ó��
    public string equipName;
    public string realName;
    [TextArea] public string description;
    public int index;
    public int[] resChange = new int[4];
}