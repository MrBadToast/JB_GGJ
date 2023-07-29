using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] itemSlots;
    [SerializeField] private DOTweenAnimation anim;

    public void SetSlots(Item_Data[] dataSet)
    {
        Deselect();
        for(int i = 0; i < dataSet.Length; i++)
        {
            itemSlots[i].GetComponent<Image>().sprite = dataSet[i].Item_sprite;
        }
    }

    public void Add_Item() { MainGameManager.Instance.AddItem(Click_Controller.instance.target.GetComponent<Dragable_Object>()?.data); Debug.Log(Click_Controller.instance.target.GetComponent<Dragable_Object>()?.data); }
    public void Get_Item(int num) 
    {
        var item = MainGameManager.Instance.GetItem(num);
        Dragable_Object d_obj = Object_Pool.SpawnFromPool<Dragable_Object>("Dragable_Object", Vector3.zero);
        d_obj.Set_Obj(item);
        d_obj.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Click_Controller.instance.target = d_obj.transform.gameObject;
        Click_Controller.instance.Dragging = true;
        MainGameManager.Instance.RemoveItemAt(num);
    }


    public void OpenItemSlot()
    {
        anim.DORestartById("Inventory_Open");
    }

    public void CloseItemSlot()
    {
        anim.DORestartById("Inventory_Close");
    }

    public void SelectSlot(int index)
    {
        Color c = itemSlots[index].GetComponent<Image>().color;
        itemSlots[index].GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0.5f);
    }

    public void Deselect()
    {
        foreach(GameObject item in itemSlots)
        {
            Color c = item.GetComponent<Image>().color;
            item.GetComponent<Image>().color = Color.white;
        }
    }
}
