using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClip_Controller : MonoBehaviour
{
    AudioSource AS;

    private void Awake()
    {
        AS = GetComponent<AudioSource>();
    }
    public void Audio_Play(AudioClip clip_)
    {
        AS.clip = clip_;
        AS.Play();
        Invoke("disable", clip_.length + 1f);
    }

    void disable() { this.gameObject.SetActive(false); }
}
