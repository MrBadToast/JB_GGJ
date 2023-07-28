using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_Controller : MonoBehaviour
{
    public static Click_Controller instance = null;
    GameObject target;
    Vector2 Click_Pos;
    bool Dragging;
    public bool Can_Interact;
    public void Awake()
    {
        instance = this;
    }
    public void Update()
    {
        // ��ȣ�ۿ� ������ ��
        if (Can_Interact)
        {
            RaycastHit2D hit;
            // Ŭ��
            if (Input.GetMouseButtonDown(0))
            {
                Dragging = false;
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 100);
                if (hit)
                {
                    Click_Pos = hit.transform.position;
                    target = hit.transform.gameObject;
                    target.GetComponent<Clickable_Object>()?.On_Click();
                    target.GetComponent<Dragable_Object>()?.On_Click();

                }
                else { target = null; }
            }
            if (Vector2.Distance(Click_Pos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.25f) { Dragging = true; }
            // �巡��
            if (Input.GetMouseButton(0) && target != null && Dragging)
            {
                // ���� �Ÿ� �̻� �̵� �� �巡�� ����
                target.GetComponent<Dragable_Object>()?.On_Drag();
                target.GetComponent<Dragable_Object>()?.Set_Pos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
            // ��
            if (Input.GetMouseButtonUp(0) && target != null)
            {
                Dragging = false;
                target.GetComponent<Dragable_Object>()?.Off_Drag();
            }
        }
    }

    public void Interact(bool isCan) { Can_Interact = isCan; }
}
