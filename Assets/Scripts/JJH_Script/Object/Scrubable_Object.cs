using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using DG.Tweening;

public class Scrubable_Object : MonoBehaviour, Item
{
    Item_Data data;
    SpriteRenderer srr;
    public float time;

    private void Awake()
    {
        srr = GetComponent<SpriteRenderer>();
    }

    public void Set_Obj(Item_Data _data)
    {
        data = _data;
        srr.sprite = data.Item_sprite;
        time = 1;
        srr.DOFade(1, 0.05f);
    }
    public void On_Scrub()
    {
        time -= data.Process_Time;
        Particle_Manager particle = Object_Pool.SpawnFromPool<Particle_Manager>("Particle", this.transform.position);
        particle.Particle_Create(data.Effect, data.Process_Time);
        srr.DOFade(time, 0.2f).SetEase(Ease.Linear);
        if (time <= 0) 
        { 
            MainGameManager.Instance.AddScore(20);
            MainGameManager.Instance.AddElapsedTime(MainGameManager.Instance.time_objectInteract); 
            RuntimeManager.PlayOneShot(data.soundOnInteract); 
            Off_Scrub();
            this.gameObject.SetActive(false); 
        }
    }
    public void Off_Scrub()
    {
        Click_Controller.instance.Interact(true);
    }
}
