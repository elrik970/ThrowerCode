using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public string[] TagsToEffect;
    public int[] TagMultipliers;
    public float DamageAmount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col) {
        
        int index = 0;
        foreach (string Tag in TagsToEffect) {
            if (col.gameObject.CompareTag(Tag)) {
                HealthClass Health = col.gameObject.GetComponent<HealthClass>();
                if (Health != null) {
                    Health.DoDamage(DamageAmount*TagMultipliers[index]);
                }
            }
            index++;
        }

        
    }
}
