using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FMODUnity;

public enum Item_Type
{
    Click,
    Drag,
    Scrub
}
#if UNITY_EDITOR

[CustomEditor(typeof(Item_Data))]

public class Item_Data_Editor : Editor
{
    const string INFO = "�̸�, �̹���, ó�� �ð��� �ʼ�\n" +
        "isFix �� ����ġ ���� �ƴϸ� ���� �� ���� ���� \n" +
        "Effect �� ó�� �� ���� ��ƼŬ �� Sound�� ���� \n" +
        "Interact_Obj �� �巡�� �� ��� �� ��ȣ�ۿ� �� ������Ʈ \n" +
        "Type �����ֱ� \n";
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox(INFO, MessageType.Info);
        base.OnInspectorGUI();
    }
}
#endif
[CreateAssetMenu]
public class Item_Data : ScriptableObject
{
    public string Item_name;
    public Sprite Item_sprite;
    public float Process_Time;
    public bool IsFix;
    public string Effect;
    public EventReference soundOnInteract;
    public string Interact_Obj;
    public Item_Type Type;
    public Material outlineColor;
}
