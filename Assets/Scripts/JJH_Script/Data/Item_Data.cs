using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item_Type
{
    Click,
    Drag,
    Toggle
}
[CreateAssetMenu]
public class Item_Data : ScriptableObject
{
    public string Item_name;
    public Sprite Item_sprite;
    public float Process_Time;
    public bool IsFix;
    public string Effect;
    public string Interact_Obj;
    public Item_Type Type;
}
