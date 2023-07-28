using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Manager : MonoBehaviour
{
    ParticleSystem[] particle;

    private void Awake()
    {
        Array.Resize(ref particle, this.transform.childCount);
        for (int i = 0; i < this.transform.childCount; i++)
        {
            particle[i] = transform.GetChild(i).GetComponent<ParticleSystem>();
            particle[i].gameObject.SetActive(false);
        }
    }

    public void Particle_Create(string name, float time)
    {
        ParticleSystem set_particle = null;
        for (int i = 0; i < particle.Length; i++)
        {
            if(particle[i].gameObject.name == name) { set_particle = particle[i]; break; }
        }
        set_particle.gameObject.SetActive(true);
        set_particle.Play();
        Invoke("disable", time + 0.5f);
    }

    void disable() { this.gameObject.SetActive(false); }
}
