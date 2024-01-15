using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float minVelocity;
    [SerializeField] float VelocityMultiplier;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("DamageDealing")) {
            Rigidbody colrb = col.gameObject.GetComponent<Rigidbody>();
            if (colrb.velocity.magnitude > minVelocity) {
                float DamageTaken = col.gameObject.GetComponent<ThrowableObjects>().ExtraDamageAmount;
                DamageTaken += (VelocityMultiplier*colrb.velocity.magnitude)+(colrb.mass);
                GetComponent<HealthClass>().DoDamage(DamageTaken);

                col.gameObject.GetComponent<ThrowableObjects>().OnHit();
            }
        }
    }
}
