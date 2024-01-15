using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldObjects : MonoBehaviour
{

    // Start is called before the first frame update
    public List<GameObject> Objects = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate() {
        Objects = new List<GameObject>();
    }
    void OnTriggerStay(Collider col) {
        if (col.GetComponent<ThrowableObjects>() != null) {
            Objects.Add(col.gameObject);
        }
    }
    // void OnTriggerExit(Collider col) {
    //     if (Objects.Contains(col.gameObject)) {
    //         Objects.Remove(col.gameObject);
    //     }
    // }
}
