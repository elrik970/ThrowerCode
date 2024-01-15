using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomClass : MonoBehaviour
{
    public int RowSize;
    public string[] spots;
    [TextArea]
    [Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string Notes = "RowSize is how big each row is, must be an odd number. ColumnSize must also be an odd number  that way it has a center. You cannot show a 2d array in inspector so instead spots is a flattened matrix that can be restored knowing how big the row size is";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract Dictionary<string,Vector2> doors();
}