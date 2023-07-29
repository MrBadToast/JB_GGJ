using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainGameManager : MonoBehaviour
{
    static private MainGameManager instance;
    public static MainGameManager Instance { get { return instance; } }

    [SerializeField] private float gameTime = 60f;
    public int score = 0;
    [SerializeField,MinMaxSlider(1.0f,5.0f)] private Vector2 randomIncidentInterval;
    [SerializeField] private float lastIncidentTime = 50f;

    private List<Item_Data> inventory = new List<Item_Data>();
    public List<Item_Data> Inventory { get { return inventory; } }

    private const int inventorySize = 5;

    public MultipleRoomsManager rooms;
    public DialogueContainer dialogueCont;

    private float elapsedTime = 0f;
    public float ElapsedTime { get { return elapsedTime; } }
    public float ElapsedTime01 { get { return Mathf.Clamp01(elapsedTime / gameTime); } }

    bool sequenceRunning = false;

    private void Awake()
    {
        if(instance == null) instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartMainGameSequence();
    }

    public bool AddItem(Item_Data item)
    {
        if(inventory.Count == inventorySize)
        {
            return false;
        }
        else
        {
            inventory.Add(item);
            UI_Manager.Instance.itemPanel.SetSlots(inventory.ToArray());
            return true;
        }
    }

    int pickedIndex = -1;
    public void PickItem(int index)
    {
        if (pickedIndex != -1) return;
        if (!TryItem(index)) return;

        UI_Manager.Instance.itemPanel.SelectSlot(index);
        pickedIndex = index;
        var item = inventory[pickedIndex];
        GameObject act_Room = null;
        foreach (Room_Data room in rooms.rooms) { if (room.gameObject.activeSelf) { act_Room = room.gameObject; break; } }
        Dragable_Object d_obj = Object_Pool.SpawnFromPool<Dragable_Object>("Dragable_Object", Vector3.zero);
        d_obj.transform.SetParent(act_Room.transform);
        d_obj.Set_Obj(item);
        d_obj.transform.position = act_Room.transform.position + Vector3.back;
        UI_Manager.Instance.itemPanel.Deselect();
        RemoveItemAt(pickedIndex);
        pickedIndex = -1;
    }

    public void StartMainGameSequence()
    {
        if (sequenceRunning) return;
        StartCoroutine(Cor_MainGameSequence());
    }

    public void InvokeIncident(int roomIndex)
    {
        rooms.SpawnIncident(roomIndex);
    }

    float incidentTimer = 0f;

    private bool TryItem(int index)
    {
        if (index > inventorySize) return false;
        if (index >= inventory.Count) return false;
        if (inventory[index] == null) return false;
        return true;
    }

    public bool RemoveItemAt(int index)
    {
        if (inventory.Count < index) return false;
        else
        {
            inventory.RemoveAt(index);
            UI_Manager.Instance.itemPanel.SetSlots(inventory.ToArray());
            return true;
        }
    }

    private IEnumerator Cor_MainGameSequence()
    {
        //yield return dialogueCont.StartCoroutine(dialogueCont.StartLargeDialogue("IntroCutscene"));

        yield return new WaitForSeconds(2f);

        yield return UI_Manager.Instance.countDown.StartCoroutine(UI_Manager.Instance.countDown.Cor_CountDown());

        UI_Manager.Instance.StartOpen();

        float nextIncidentTime = Random.Range(randomIncidentInterval.x, randomIncidentInterval.y);

        while(elapsedTime > gameTime)
        {
            if (incidentTimer < lastIncidentTime)
            {
                incidentTimer += Time.deltaTime;
            }

            if(incidentTimer > nextIncidentTime)
            {
                InvokeIncident(Random.Range(0, rooms.RoomCount));
                incidentTimer = 0f;
                nextIncidentTime = Random.Range(randomIncidentInterval.x, randomIncidentInterval.y);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


}
