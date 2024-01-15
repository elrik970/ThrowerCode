using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null) {
            transform.right = Player.position-transform.position;
        }
        else {
            Player = GameObject.FindWithTag("Player").transform;
        }
    }
}
