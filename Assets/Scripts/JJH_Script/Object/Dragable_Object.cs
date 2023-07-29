using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable_Object : MonoBehaviour
{
    public Item_Data data;

    SpriteRenderer srr;
    Collider2D col;

    bool Is_Drag;

    public void Awake()
    {
        col = GetComponent<Collider2D>();
        srr = GetComponent<SpriteRenderer>();
    }
    public void Set_Obj(Item_Data _data)
    {
        data = _data;
        srr.sprite = data.Item_sprite;
    }
    public void On_Click() { Is_Drag = false; }
    public void On_Drag()
    {
        Is_Drag = true;
        col.enabled = false;
    }
    public void Set_Pos(Vector2 pos) { this.transform.position = pos; }
    public GameObject Off_Drag() 
    {
        if (!Is_Drag) { MainGameManager.Instance.AddItem(data); this.gameObject.SetActive(false); }
        GameObject target;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 100);
        if (hit)
        {
            target = hit.transform.gameObject;
            if(target.name == data.Interact_Obj) { StartCoroutine(Clean(data.Process_Time)); }

        }
        else { target = null; }
        col.enabled = true;

        return target;
    }

    IEnumerator Clean(float time)
    {
        Click_Controller.instance.Interact(false);
        Particle_Manager particle = Object_Pool.SpawnFromPool<Particle_Manager>("Particle", this.transform.position);
        particle.Particle_Create(data.Effect, data.Process_Time);
        yield return new WaitForSeconds(time);
        Click_Controller.instance.Interact(true);
        this.gameObject.SetActive(false);
    }
}
