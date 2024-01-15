using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera Camera;
    void Start()
    {
        Camera = Camera.main;   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(Camera.transform.position);
    }
}
