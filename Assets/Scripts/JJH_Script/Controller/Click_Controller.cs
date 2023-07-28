using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_Controller : MonoBehaviour
{
    GameObject target;
    public void Update()
    {
        RaycastHit2D hit;
        // 클릭
        if (Input.GetMouseButtonDown(0))
        {

            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 100);
            if (hit)
            {
                target = hit.transform.gameObject;
                target.GetComponent<Clickable_Object>()?.On_Click();
            }
            else { target = null;}
        }
        // 드래그
        if (Input.GetMouseButton(0) && target != null)
        {
            target.GetComponent<Dragable_Object>()?.On_Drag();
            target.GetComponent<Dragable_Object>()?.Set_Pos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        // 끝
        if (Input.GetMouseButtonUp(0) && target != null)
        {
            target.GetComponent<Dragable_Object>()?.Off_Drag();
        }
    }
}
