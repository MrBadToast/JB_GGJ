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
        On_Click();
    }
    public void On_Click()
    {
        if((!Toggle && Can_On) || Toggle)
        {
            Toggle = (Toggle ? false : true);
            if (Toggle) { OFF.DOFade(0, 0.6f).SetEase(Ease.Linear); ON.DOFade(1, 0.6f).SetEase(Ease.Linear); }
            else { OFF.DOFade(1, 0.6f).SetEase(Ease.Linear); ON.DOFade(0, 0.6f).SetEase(Ease.Linear); }
        }
    }

    public void Change_Toggle(bool _Toggle)
    {
        Toggle = _Toggle;
    }
}
