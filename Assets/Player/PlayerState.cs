using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerState<T> : ScriptableObject where T : MonoBehaviour
{
    public T runner;
    public Player player;
    public Rigidbody rb;
    
    public virtual void Init(T parent) {
        runner = parent;
        player = parent.GetComponent<Player>();
        rb = parent.GetComponent<Rigidbody>();
    }

    public abstract void ConstantUpdate();
    public abstract void CaptureInputs();
    public abstract void ChangeState();
    public abstract void PhysicsUpdate();
    public abstract void Exit();
    
}
