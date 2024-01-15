using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxTime;
    private float curTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curTime+=Time.deltaTime;
        if (curTime > maxTime) {
            Destroy(gameObject);
        }
    }
}
