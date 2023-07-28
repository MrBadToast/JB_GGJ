using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] itemSlots;

    public void SetSlots(Item_Data[] dataSet)
    {
        Deselect();
       for(int i = 0; i < dataSet.Length; i++)
        {
            itemSlots[i].GetComponent<Image>().sprite = dataSet[i].Item_sprite;
        }
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
