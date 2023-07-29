using FMODUnity;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Timers;
using UnityEngine;
using UnityEngine.UIElements;

public class MainGameManager : MonoBehaviour
{
    static private MainGameManager instance;
    public static MainGameManager Instance { get { return instance; } }

    [SerializeField] private float fullGameTime = 60f*60f*9f;
    [SerializeField] private float timeSpeedMultiplier = 30f;
    [SerializeField] private int maxScore = 1000;
    [SerializeField] private int score = 0;
    public int Score { get { return score; } }

    public EventReference sound_earthquake;
    public EventReference sound_storm;
    public EventReference sound_pest;
    public EventReference sound_dog;

    public float scoreDiminishTime = 60f * 60f;
    [Title("RegularIncounter Info")]
    public float regularIncounter = 60f * 30f;
    public float incounterRate = 0.66f;

    [Title("TimeUse")]
    public float time_roomMove = 30f;
    public float time_objectInteract = 40f;

    private List<Item_Data> inventory = new List<Item_Data>();
    public List<Item_Data> Inventory { get { return inventory; } }

    private const int inventorySize = 3;

    public MultipleRoomsManager rooms;
    public DialogueContainer dialogueCont;

    private float elapsedTime = 0;
    public float ElapsedTime { get { return elapsedTime; } }
    public float ElapsedTime01 { get { return Mathf.Clamp01(elapsedTime / fullGameTime); } }

    bool sequenceRunning = false;
    private StudioEventEmitter sound_ClockTicking;

    private void Awake()
    {
        if(instance == null) instance = this;
        else
        {
            Destroy(gameObject);
        }

        sound_ClockTicking = GetComponent<StudioEventEmitter>();
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

    public void AddScore(int value)
    {
        if (score + value < 0)
        { score = 0; UI_Manager.Instance.score.SetCurrentScore(score); }
        else if (score + value > 1000) { score = 1000; UI_Manager.Instance.score.SetCurrentScore(score,true); }
        else { score += value; UI_Manager.Instance.score.SetCurrentScore(score); }
    }

    public void AddElapsedTime(float value)
    {
        elapsedTime += value;
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

        float regularIncounterTimer = 0f;
        float scoreDiminishTimer = 0f;

        sound_ClockTicking.Play();


        while (elapsedTime < fullGameTime)
        {

            sound_ClockTicking.EventInstance.setParameterByName("Volume", Mathf.InverseLerp(0f, scoreDiminishTime, scoreDiminishTimer));
            //regular incounter
            if(scoreDiminishTime < scoreDiminishTimer)
            {
                

                scoreDiminishTimer = 0f;
            }

            if (regularIncounter < regularIncounterTimer)
            {
                if (Random.Range(0f, 1f) < incounterRate)
                {
                    int task = Random.Range(0, 3);
                    switch (task)
                    {
                        case 0:
                            UI_Manager.Instance.alert.InvokeAlert("지진 발생! 집안이 마구 흔들립니다!");
                            RuntimeManager.PlayOneShot(sound_earthquake);
                            break;
                        case 1:
                            UI_Manager.Instance.alert.InvokeAlert("폭우 발생! 집에 물난리가 납니다!");
                            RuntimeManager.PlayOneShot(sound_storm);
                            break;
                        case 2:
                            UI_Manager.Instance.alert.InvokeAlert("비상! 벌레가 집을 더럽힙니다!");
                            RuntimeManager.PlayOneShot(sound_pest);
                            break;
                        case 3:
                            UI_Manager.Instance.alert.InvokeAlert("강아지 탈출! 강아지가 집을 어지럽힙니다!");
                            RuntimeManager.PlayOneShot(sound_dog);
                            break;
                    }
                }

                regularIncounterTimer = 0f;
            }

            score = Mathf.Clamp(score, 0, maxScore);


            UI_Manager.Instance.clock.SetClock(ElapsedTime01);
            regularIncounterTimer += Time.deltaTime * timeSpeedMultiplier;
            scoreDiminishTimer += Time.deltaTime * timeSpeedMultiplier;
            elapsedTime += Time.deltaTime * timeSpeedMultiplier;
            yield return null;

        }

        sound_ClockTicking.Stop();

    }
}
