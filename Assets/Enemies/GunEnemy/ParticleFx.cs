using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFx : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject particleFx; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy() {
        GameObject.Instantiate(particleFx,transform.position,Quaternion.identity);
    }
}
