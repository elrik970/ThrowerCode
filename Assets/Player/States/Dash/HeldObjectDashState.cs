using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New HeldObjectDashState", menuName = "States/Player/HeldObjectDashState")]
public class HeldObjectDashState : PlayerState<Player>
{
    [SerializeField] PlayerState<Player> IdleState;
    public float Accel;
    public float maxSpeed;
    public float OriginalMaxSpeed;
    private PlayerInputs Inputs;
    private float curTime = 0f;
    [SerializeField] float maxTime = 0.3f;
    private Vector3 MovementVector; 
    
    // On Start
    public override void Init(Player parent) {
        base.Init(parent);
        MovementVector = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical")).normalized;
        rb.velocity = new Vector3(MovementVector.x*maxSpeed,0f,MovementVector.z*maxSpeed);
        curTime = 0f;
        maxSpeed = OriginalMaxSpeed-(player.HeldObject.GetComponent<Rigidbody>().mass);
    }
    // Unity Update
    public override void ConstantUpdate() {
        curTime+=Time.deltaTime;
        if (curTime > maxTime) {
            runner.GetComponent<PlayerStateRunner>().SetState(IdleState);
        }
    }
    // Unity Update
    public override void ChangeState() {
        
    }
    // Unity Update
    public override void CaptureInputs() {

    }
    // Unity FixedUpdate
    public override void PhysicsUpdate() {
        rb.velocity = new Vector3(MovementVector.x*maxSpeed,0f,MovementVector.z*maxSpeed);
    }
    public override void Exit() {

    }
}
