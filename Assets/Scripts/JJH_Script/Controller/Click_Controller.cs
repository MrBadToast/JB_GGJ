using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_Controller : MonoBehaviour
{
    public static Click_Controller instance = null;
    public GameObject target;
    public Vector2 Click_Pos;
    Vector2 Scrub_Pos;
    public bool Getting;
    public int Get_num;
    public bool Dragging;
    bool Scrubbing;
    public bool Can_Interact;
    public void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void Update()
    {
        // 상호작용 가능할 때
        if (Can_Interact)
        {
            RaycastHit2D hit;
            // 클릭
            if (Input.GetMouseButtonDown(0))
            {
                Dragging = false;
                Scrubbing = false;
                Getting = false;
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 100);
                if (hit)
                {
                    Click_Pos = hit.transform.position;
                    Scrub_Pos = hit.transform.position;
                    target = hit.transform.gameObject;
                    Debug.Log($"{target.transform.gameObject}를 눌렀다!");
                    target.GetComponent<Clickable_Object>()?.On_Click();
                    target.GetComponent<Dragable_Object>()?.On_Click();
                    target.GetComponent<Toggle_Object>()?.On_Click();
                }
                else { target = null; }
            }
            if (Vector2.Distance(Click_Pos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > 0.25f) { Dragging = true; }
            
            // 드래그
            if (Input.GetMouseButton(0) && target != null && target.activeSelf && Dragging)
            {
                // 일정 거리 이상 이동 시 드래그 적용
                target.GetComponent<Dragable_Object>()?.On_Drag();
                target.GetComponent<Dragable_Object>()?.Set_Pos(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                // 비비기
                if (target != null && target.activeSelf)
                {
                    hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 100);
                    if (!hit && !Scrubbing) { Scrubbing = true; target.GetComponent<Scrubable_Object>()?.On_Scrub(); Debug.Log($"{target.transform.gameObject}를 비볐다!"); }
                    else if(hit && Scrubbing) { Scrubbing = false; }
                }
            }
            // 끝
            if (Input.GetMouseButtonUp(0) && target != null)
            {
                // 혹시 꺼낸 아이템이라면
                if (Getting)
                {
                    MainGameManager.Instance.RemoveItemAt(Get_num);
                }
                Debug.Log($"{target.transform.gameObject}를 놨다!");
                Dragging = false;
                Scrubbing = false;
                target.GetComponent<Dragable_Object>()?.Off_Drag();
                target.GetComponent<Scrubable_Object>()?.Off_Scrub();
            }
        }
    }

    public void Interact(bool isCan) { Can_Interact = isCan; }
}
