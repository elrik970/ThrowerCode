using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUsedObject : UsedObjectClass
{
    [SerializeField] ParticleSystem GunParticleFx;
    [SerializeField] AudioSource SoundFx;
    [SerializeField] float soundFxStartTime;
    public override void OnUse() {
        GunParticleFx.Play();
        SoundFx.Play();
        SoundFx.time = soundFxStartTime;
    }
}
