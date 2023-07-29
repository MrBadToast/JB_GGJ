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

    private void Start()
    {
        foreach(GameObject i in itemSlots)
        {
            i.GetComponent<Image>().color = new Color(1f,1f,1f,0f);
        }
    }

    public void SetSlots(Item_Data[] dataSet)
    {
        Deselect();
        for(int i = 0; i < itemSlots.Length; i++)
        {
            foreach (GameObject it in itemSlots)
            {
                it.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            }

            if (i < dataSet.Length) { itemSlots[i].GetComponent<Image>().sprite = dataSet[i].Item_sprite; }
            else { itemSlots[i].GetComponent<Image>().color = new Color(1, 1, 1, 1); itemSlots[i].GetComponent<Image>().sprite = null; }
        }
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
