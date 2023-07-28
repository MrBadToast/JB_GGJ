using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Alert : MonoBehaviour
{
    [SerializeField] DOTweenAnimation doAnimation;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] float windowStayTime = 3.0f;

    public void InvokeAlert(string context)
    {
        StopAllCoroutines();
        StartCoroutine(Cor_InvokeAlert(context, windowStayTime));
    }

    IEnumerator Cor_InvokeAlert(string context,float alertTime)
    {
        textMesh.text = context;
        doAnimation.DORestartById("Alert_Open");
        Tween tw = doAnimation.GetTweens()[0];

        yield return tw.WaitForCompletion();
        yield return new WaitForSeconds(windowStayTime);

        doAnimation.DORestartById("Alert_Close");

    }
}
