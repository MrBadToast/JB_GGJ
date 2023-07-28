using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmediateEscape : MonoBehaviour
{
    public GameObject TextObject;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    float t = 0;
    float hold = 3.0f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            t += Time.deltaTime;
            TextObject.SetActive(true);
            if (t > hold) Application.Quit();
        }
        else
        {
            t = 0f;
            TextObject.SetActive(false);
        }
    }
}
