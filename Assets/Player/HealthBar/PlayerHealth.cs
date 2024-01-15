using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : HealthClass
{
    // Start is called before the first frame update
    public ParticleSystem HitFX;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0) {
            Death();
        }
        if (Health>MaxHealth) {
            Health = MaxHealth;
        }
    }
    public override void DoDamage(float DamageAmount) {
        base.DoDamage(DamageAmount);
        HitFX.Play();
    }
    public override void Death() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
