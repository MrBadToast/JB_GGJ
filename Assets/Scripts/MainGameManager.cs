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

    public Item_Data GetItem(int num)
    {
        return inventory[num];
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
