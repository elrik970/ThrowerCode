using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHitThrowableObject : ThrowableObjects
{
    // Start is called before the first frame update
    public GameObject effects;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnPickup() {
        transform.position = new Vector3(0,100f,0);
        Destroy(gameObject);
    }
    public override void OnHit() {
        Destroy(gameObject);
        GameObject.Instantiate(effects,transform.position,Quaternion.identity);
    }
}
