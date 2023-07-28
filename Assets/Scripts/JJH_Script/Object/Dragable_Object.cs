using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable_Object : MonoBehaviour
{
    public void On_Drag() { Debug.Log("onDrag"); }
    public void Set_Pos(Vector2 pos) { this.transform.position = pos; }
    public void Off_Drag() { Debug.Log("offDrag"); }
}
