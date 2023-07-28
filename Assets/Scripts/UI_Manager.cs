using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    private UI_Manager instance;
    public UI_Manager Instance { get { return instance; } }

    public UI_ItemPanel itemPanel;
    public UI_Clock clock;
    public UI_Alert alert;
    public UI_BottomButtons bottomButtons;

    private void Awake()
    {
        if(instance == null) instance = this;
        else { Destroy(gameObject); }
    }


}
