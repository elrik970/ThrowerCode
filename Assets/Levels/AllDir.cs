using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDir : RoomClass
{
    // public Dictionary<string, Vector2> doors = new Dictionary<string,Vector2>() 
    // {
    //     {"leftDoor", new Vector2(-1,0)},
    //     {"upDoor", new Vector2(0,1)},
    //     {"downDoor", new Vector2(0,-1)},
    //     {"rightDoor", new Vector2(1,0)}
    // };
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override Dictionary<string,Vector2> doors() {
        return new Dictionary<string,Vector2>(){
        {"leftDoor", new Vector2(-1,0)},
        {"upDoor", new Vector2(0,1)},
        {"downDoor", new Vector2(0,-1)},
        {"rightDoor", new Vector2(1,0)}
        };
    }
}
