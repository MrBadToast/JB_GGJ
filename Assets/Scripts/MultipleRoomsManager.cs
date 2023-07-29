using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleRoomsManager : MonoBehaviour
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

}
