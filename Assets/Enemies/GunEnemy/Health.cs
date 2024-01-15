using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : HealthClass
{
    // Start is called before the first frame update
    public GameObject SoundFx;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health < 0) {
            Death();
        }
    }
    public override void DoDamage(float DamageAmount) {
        base.DoDamage(DamageAmount);
        OnHit();
    }
    public override void Death() {
        base.Death();
    }
    void OnHit() {
        GameObject.Instantiate(SoundFx,transform.position,Quaternion.identity);
    }
}
