using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler_Disable : MonoBehaviour
{
    // Start is called before the first frame update
    void OnDisable()
    {
        Object_Pool.ReturnToPool(this.gameObject);
    }
}
