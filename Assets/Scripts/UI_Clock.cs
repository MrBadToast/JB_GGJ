using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Clock : MonoBehaviour
{
    [SerializeField] Transform hour;
    [SerializeField] Transform minute;
    [SerializeField] new Image renderer;
    [SerializeField] DOTweenAnimation doAnimation;

    public void SetClock(float value)
    {
        renderer.material.SetFloat("_Value", Mathf.Lerp(0.25f,1.0f,value));
        hour.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(90f,-180f,value)); ;
        minute.rotation = Quaternion.Euler(0f, 0f, -value * 360f * 9f);
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
