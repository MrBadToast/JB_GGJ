using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] itemSlots;

    public void SetSlots(Item_Data[] dataSet)
    {
       for(int i = 0; i < dataSet.Length; i++)
        {
            itemSlots[i].GetComponent<Image>().sprite = dataSet[i].Item_sprite;
        }
    }
}
