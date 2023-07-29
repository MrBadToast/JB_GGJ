using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Toggle_Object : MonoBehaviour
{
    Item_Data data;
    SpriteRenderer srr;
    bool Toggle;

    private void Awake()
    {
        srr = GetComponent<SpriteRenderer>();
    }

    public void Set_Obj(Item_Data _data)
    {
        data = _data;
        srr.sprite = data.Item_sprite;
    }

    public void On_Click()
    {
        Toggle = (Toggle ? false : true);
        if (Toggle) { srr.DOColor(Color.blue, 0.6f).SetEase(Ease.Linear); }
        else { srr.DOColor(Color.red, 0.6f).SetEase(Ease.Linear); }
    }
}
