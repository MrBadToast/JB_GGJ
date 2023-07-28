using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] private float gameTime = 60f;

    private IEnumerator Cor_MainGameSequence()
    {
        yield return null;
    }
}
