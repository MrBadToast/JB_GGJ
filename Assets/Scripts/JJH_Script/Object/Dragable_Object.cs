using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable_Object : MonoBehaviour
{
    Collider2D col;

    public void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    public void On_Click() { Debug.Log("인벤토리 넣기"); }
    public void On_Drag()
    { 
        col.enabled = false;
        Debug.Log("onDrag"); 
    }
    public void Set_Pos(Vector2 pos) { this.transform.position = pos; }
    public GameObject Off_Drag() 
    {
        GameObject target;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 100);
        if (hit)
        {
            target = hit.transform.gameObject;
            Debug.Log(target);

        }
        else { target = null; }

        Debug.Log("offDrag");
        col.enabled = true;

        return target;
    }
}
