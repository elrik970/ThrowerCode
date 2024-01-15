using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthClass : MonoBehaviour
{
    // Start is called before the first frame update
    public float MaxHealth;
    public float Health;
    public virtual void DoDamage(float DamageAmount) {
        Health-=DamageAmount;
    }
    public virtual void Death() {
        Destroy(gameObject);
    }

}