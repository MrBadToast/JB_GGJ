using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI goalScoreText;
    [SerializeField] private DOTweenAnimation textAnim;

    private DOTweenAnimation doAnimation;

    public void OpenScore()
    {
        doAnimation.DORestartById("Score_Open");
    }

    public void CloseScore()
    {
        doAnimation.DORestartById("Score_Close");
    }

    public void SetGoalScore(int score)
    {
        goalScoreText.text = score.ToString();
    }

    public void SetCurrentScore(int score,bool large = false)
    {
        textAnim.DORestart();
        currentScoreText.text = score.ToString();
        if (large) currentScoreText.fontSize = 100f;
        else currentScoreText.fontSize = 80f;
    }
}
