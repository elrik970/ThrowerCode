using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeldObjectAbstractClass : MonoBehaviour
{
    public GameObject ThrownObject;
    public float ThrowForce;
    public float YThrowForce;
    public Vector3 StartingRotation = Vector3.zero;
}
