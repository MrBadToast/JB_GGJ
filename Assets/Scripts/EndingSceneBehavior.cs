using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneBehavior : MonoBehaviour
{
    public DialogueContainer dialogue;

    private void Start()
    {
        dialogue.StartCoroutine(dialogue.StartLargeDialogue("Ending"));
    }

    public void RestartEntireGame()
    {
        LoadingSceneController.Instance.LoadScene("MainTitle");
    }
}
