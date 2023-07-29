using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    static private UI_Manager instance;
    static public UI_Manager Instance { get { return instance; } }

    public UI_ItemPanel itemPanel;
    public UI_Clock clock;
    public UI_Alert alert;
    public UI_BottomButtons bottomButtons;
    public UI_CountDown countDown;
    public UI_LargeDialogue cutscene;

    private void Awake()
    {
        if(instance == null) instance = this;
        else { Destroy(gameObject); }
    }

    public void StartOpen()
    {
        itemPanel.OpenItemSlot();
        clock.OpenClock();
        bottomButtons.OpenButtons();
    }

}
