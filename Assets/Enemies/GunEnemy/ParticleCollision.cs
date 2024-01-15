using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    public float DamageAmount;
    public string[] TagsToEffect;
    [SerializeField] GameObject HitFx;
    ParticleSystem part;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnParticleCollision(GameObject col) {
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        part.GetCollisionEvents(col, collisionEvents);

        foreach (string Tag in TagsToEffect) {
            if (col.gameObject.CompareTag(Tag)) {
                HealthClass Health = col.gameObject.GetComponent<HealthClass>();
                if (Health != null) {
                    Health.DoDamage(DamageAmount);
                }
            }
        }

        if (HitFx != null) {
            GameObject.Instantiate(HitFx,collisionEvents[0].intersection,Quaternion.identity);
        }

        
    }
}
