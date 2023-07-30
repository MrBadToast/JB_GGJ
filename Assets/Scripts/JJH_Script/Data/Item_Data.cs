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
    const string INFO = "이름, 이미지, 처리 시간은 필수\n" +
        "isFix 는 한위치 고정 아니면 범위 내 랜덤 생성 \n" +
        "Effect 는 처리 시 나올 파티클 명 Sound는 사운드 \n" +
        "Interact_Obj 는 드래그 앤 드롭 시 상호작용 될 오브젝트 \n" +
        "Type 정해주기 \n";
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
