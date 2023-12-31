using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Toggle_Object : MonoBehaviour
{
    public SpriteRenderer OFF;
    public SpriteRenderer ON;
    public bool Toggle;
    public bool Can_On;
    private void Awake()
    {
        Change_Toggle(true);
    }
    public void On_Click()
    {
        if((!Toggle && Can_On) || Toggle)
        {
            if (!Can_On) { MainGameManager.Instance.AddScore(20); }
            MainGameManager.Instance.AddElapsedTime(MainGameManager.Instance.time_objectInteract);
            Click_Controller.instance.Can_Interact = false;
            Invoke("Can_Interact", 1f);
            Toggle = (Toggle ? false : true);
            set_Toggle();
        }
    }

    public void Can_Interact() { Click_Controller.instance.Can_Interact = true; }

    public void Change_Toggle(bool _Toggle)
    {
        Toggle = _Toggle;
        set_Toggle();
    }

    void set_Toggle()
    {
        if (Toggle) { 
            OFF.DOFade(0, 0.6f).SetEase(Ease.Linear).OnComplete(() => OFF.gameObject.SetActive(false));
            ON.gameObject.SetActive(true);
            ON.DOFade(1, 1f).SetEase(Ease.Linear); 
        }
        else {
            OFF.gameObject.SetActive(true);
            OFF.DOFade(1, 0.6f).SetEase(Ease.Linear);
            ON.DOFade(0, 1f).SetEase(Ease.Linear).OnComplete(() => ON.gameObject.SetActive(false)); 
        }
    }
}
