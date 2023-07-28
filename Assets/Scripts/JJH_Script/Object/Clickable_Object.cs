using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable_Object : MonoBehaviour
{
    public Item_Data data;
    public void On_Click() 
    {
        StartCoroutine(Click_Co(data.Process_Time));
    }

    IEnumerator Click_Co(float time)
    {
        Click_Controller.instance.Interact(false);
        Particle_Manager particle = Object_Pool.SpawnFromPool<Particle_Manager>("Particle", this.transform.position);
        particle.Particle_Create(data.Effect, data.Process_Time);
        yield return new WaitForSeconds(time);
        Click_Controller.instance.Interact(true);
        this.gameObject.SetActive(false);
    }
}
