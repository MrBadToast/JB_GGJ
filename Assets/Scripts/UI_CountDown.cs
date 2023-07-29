using DG.Tweening;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_CountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private DOTweenAnimation textAnim;
    [SerializeField] private EventReference sound_CountTick;
    [SerializeField] private EventReference sound_Start;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        canvasGroup.alpha = 0f;
    }

    public IEnumerator Cor_CountDown()
    {
        canvasGroup.alpha = 1f;
        textAnim.DORestart();
        textMesh.text = "3";
        RuntimeManager.PlayOneShot(sound_CountTick);

        yield return new WaitForSeconds(1f);
        textAnim.DORestart();
        textMesh.text = "2";
        RuntimeManager.PlayOneShot(sound_CountTick);

        yield return new WaitForSeconds(1f);
        textAnim.DORestart();
        textMesh.text = "1";
        RuntimeManager.PlayOneShot(sound_CountTick);

        yield return new WaitForSeconds(1f);
        textAnim.DORestart();
        textMesh.text = "GO!";
        RuntimeManager.PlayOneShot(sound_Start);

        yield return new WaitForSeconds(1f);

        canvasGroup.alpha = 0f;
    }
}
