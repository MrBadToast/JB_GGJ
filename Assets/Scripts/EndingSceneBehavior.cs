using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneBehavior : MonoBehaviour
{
    public DialogueContainer dialogue;

    private void Start()
    {
        StartCoroutine(Cor_Delayed());
    }

    public void RestartEntireGame()
    {
        LoadingSceneController.Instance.LoadScene("MainTitle");
    }

    IEnumerator Cor_Delayed()
    {
        yield return new WaitForEndOfFrame();
        dialogue.StartCoroutine(dialogue.StartLargeDialogue("Ending"));
    }
}
