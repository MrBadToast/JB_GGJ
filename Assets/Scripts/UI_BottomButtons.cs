using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BottomButtons : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation doAnimation;

    public void OpenButtons()
    {
        doAnimation.DORestartById("Bottom_Open");
    }

    public void CloseButton()
    {
        doAnimation.DORestartById("Bottom_Close");
    }

    public void RightRoom()
    {

    }

    public void LeftRoom()
    {

    }
}
