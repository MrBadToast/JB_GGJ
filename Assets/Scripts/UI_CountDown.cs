using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_CountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private DOTweenAnimation textAnim;
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

        yield return new WaitForSeconds(1f);
        textAnim.DORestart();
        textMesh.text = "2";

        yield return new WaitForSeconds(1f);
        textAnim.DORestart();
        textMesh.text = "1";

        yield return new WaitForSeconds(1f);
        textAnim.DORestart();
        textMesh.text = "GO!";

        yield return new WaitForSeconds(1f);

        canvasGroup.alpha = 0f;
    }
}
