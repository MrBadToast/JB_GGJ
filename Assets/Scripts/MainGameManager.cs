using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    static private MainGameManager instance;
    public static MainGameManager Instance { get { return instance; } }

    [SerializeField] private float gameTime = 60f;
    public int score = 0;

    private List<Item_Data> inventory = new List<Item_Data>();
    public List<Item_Data> Inventory { get { return inventory; } }

    private const int inventorySize = 5;

    public MultipleRoomsManager rooms;

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

    private IEnumerator Cor_MainGameSequence()
    {
        sequenceRunning = true;

        yield return UI_Manager.Instance.countDown.StartCoroutine(UI_Manager.Instance.countDown.Cor_CountDown());

        UI_Manager.Instance.StartOpen();

        while(elapsedTime > gameTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


}
