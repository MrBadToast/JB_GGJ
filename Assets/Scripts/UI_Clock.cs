using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Clock : MonoBehaviour
{
    [SerializeField] new Image renderer;
    [SerializeField] DOTweenAnimation doAnimation;

    public void SetClock(float value)
    {
        renderer.material.SetFloat("_Value", value);
    }

    public void OpenClock()
    {
        doAnimation.DORestartById("Timer_Open");
    }

    public void CloseClock()
    {
        doAnimation.DORestartById("Timer_Close");
    }

    public void ToggleTimerWarning(bool value)
    {
        if (value) { doAnimation.DOPlayById("Timer_Warning"); }
        else { doAnimation.DOPauseAllById("Timer_Warning"); }
    }
}
