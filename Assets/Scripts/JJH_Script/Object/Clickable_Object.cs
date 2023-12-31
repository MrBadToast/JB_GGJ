using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public interface Item { }
public class Clickable_Object : MonoBehaviour, Item
{
    Item_Data data;
    SpriteRenderer srr;

    private void Awake()
    {
        srr = GetComponent<SpriteRenderer>();
    }
    
    public void Set_Obj(Item_Data _data)
    {
        data = _data;
        srr.sprite = data.Item_sprite;
    }
    public void On_Click()
    {
        StartCoroutine(Clean(data.Process_Time));
    }

    IEnumerator Clean(float time)
    {
        Click_Controller.instance.Interact(false);
        RuntimeManager.PlayOneShot(data.soundOnInteract);
        MainGameManager.Instance.AddScore(20);
        MainGameManager.Instance.AddElapsedTime(MainGameManager.Instance.time_objectInteract);
        Particle_Manager particle = Object_Pool.SpawnFromPool<Particle_Manager>("Particle", this.transform.position);
        particle.Particle_Create(data.Effect, data.Process_Time);

        //AudioClip_Controller AC = Object_Pool.SpawnFromPool<AudioClip_Controller>("AudioClip_Controller", this.transform.position);
        //AC.Audio_Play(data.audioClip);
        yield return new WaitForSeconds(time);
        Click_Controller.instance.Interact(true);
        this.gameObject.SetActive(false);
    }
}
