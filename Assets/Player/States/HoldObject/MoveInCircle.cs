using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInCircle : MonoBehaviour
{

    public float RotationSpeed = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.rotation = Quaternion.AngleAxis(RotationSpeed, Vector3.up);
        transform.Rotate(0, RotationSpeed, 0, Space.Self);
    }
}
