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
        if (dialogueUI == null) { Debug.LogError("UI_LargeDialogue�� �����ϴ� ���ӿ�����Ʈ�� ã�� �� �����ϴ�. �ش� ��ũ��Ʈ�� ���� ������Ʈ�� �Բ� ����� �ּ���."); yield break; }

        yield return dialogueUI.StartCoroutine(dialogueUI.Cor_PlayDialogue(cutscene_dialogues[key]));
    }
}
