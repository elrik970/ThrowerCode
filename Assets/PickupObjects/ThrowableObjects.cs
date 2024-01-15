using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThrowableObjects : MonoBehaviour
{
    public GameObject ObjectToHold;
    public float ExtraDamageAmount = 0f;
    
    
    public virtual void OnPickup() {
        transform.position = new Vector3(0,100f,0);
        Destroy(gameObject);
    }

    public abstract void OnHit();
    
}