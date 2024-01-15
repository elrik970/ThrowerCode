using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomObj : MonoBehaviour
{
    [SerializeField] GameObject[] Objs;
    void Start()
    {
        int index = Random.Range(0,Objs.Length);
        GameObject.Instantiate(Objs[index],transform.position,Quaternion.identity);
    }
}
