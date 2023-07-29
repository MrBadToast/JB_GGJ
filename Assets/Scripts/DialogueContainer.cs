using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainer : SerializedMonoBehaviour
{
    [SerializeField] private Dictionary<string, LargeDialogueData[]> cutscene_dialogues;

    public IEnumerator StartLargeDialogue(string key)
    {
        UI_LargeDialogue dialogueUI = GameObject.FindObjectOfType<UI_LargeDialogue>();
        if (dialogueUI == null) { Debug.LogError("UI_LargeDialogue를 포함하는 게임오브젝트를 찾을 수 없습니다. 해당 스크립트를 가진 오브젝트와 함께 사용해 주세요."); yield break; }

        yield return dialogueUI.StartCoroutine(dialogueUI.Cor_PlayDialogue(cutscene_dialogues[key]));
    }
}
