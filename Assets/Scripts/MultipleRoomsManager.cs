using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleRoomsManager : SerializedMonoBehaviour
{
    public Room_Data[] rooms;
    public int RoomCount { get { return rooms.Length; } }

    [SerializeField,ReadOnly] private int currentRoomIndex = 0;
    public int CurrentRoomIndex { get { return currentRoomIndex; } }

    public void SpawnIncident(int index)
    {
        rooms[index].Spawn_Item();
    }

    public void SetToNextRoom()
    {
        if (!Click_Controller.instance.Can_Interact) return;

        rooms[currentRoomIndex].gameObject.SetActive(false);
        if (currentRoomIndex == rooms.Length-1)
        {
            currentRoomIndex = 0;
        }else
        {
            currentRoomIndex++;
        }
        rooms[currentRoomIndex].gameObject.SetActive(true);
    }

    public void SetToPreviousRoom()
    {
        if (!Click_Controller.instance.Can_Interact) return;

        rooms[currentRoomIndex].gameObject.SetActive(false);
        if (currentRoomIndex == 0)
        {
            currentRoomIndex = rooms.Length-1;
        }
        else
        {
            currentRoomIndex--;
        }
        rooms[currentRoomIndex].gameObject.SetActive(true);
    }
    [Button("Calc_Item")]
    // 맵 순회하며 어질러진 갯수를 체크해 반환
    public int Check_Room_Data()
    {
        int toggle_count = 0;
        int Item_Count = 0;
        foreach (Room_Data _data in rooms)
        {
            foreach(string key in _data.Toggles.Keys)
            {
                //if (key == "Window" && 환경이상) { }
                if (_data.Toggles[key].Toggle) { toggle_count++; }
            }
            for(int i = 0; i < _data.transform.childCount; i++)
            {
                Item _out = null;
                if (_data.transform.GetChild(i).TryGetComponent<Item>(out _out) && _data.transform.GetChild(i).gameObject.activeSelf) { Item_Count++; }
            }
        }
        return toggle_count + Item_Count;
    }
}
