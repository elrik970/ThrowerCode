using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionIsPlayers : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
    }
}
