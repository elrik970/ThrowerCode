using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : ThrowableObjects
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnPickup() {
        Destroy(gameObject);
    }
    public override void OnHit() {
        
    }
}
