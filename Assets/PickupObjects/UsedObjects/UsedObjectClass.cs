using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsedObjectClass : MonoBehaviour
{
    public float NumOfUsedTimes; 
    public abstract void OnUse();
}
