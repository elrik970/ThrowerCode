using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New IdleState", menuName = "States/Player/IdleState")]
public class IdleState : PlayerState<Player>
{
    public PlayerState<Player> MoveState;
    public PlayerState<Player> HoldingObjectState;
    public PlayerState<Player> DashState;
    public float DeAccelRate;
    private PlayerInputs Inputs;
    // On Start
    public override void Init(Player parent) {
        base.Init(parent);
        Inputs = player.Inputs;
        Inputs.Player.GrabObject.performed += OnGrab;
        // Inputs.Player.Dash.performed += OnDash;
    }
    // Unity Update
    public override void ConstantUpdate() {
        player.RotateToCursor();
    }
    // Unity Update
    public override void ChangeState() {
        
    }
    // Unity Update
    public override void CaptureInputs() {
        if (Input.GetAxis("Horizontal") != 0||Input.GetAxis("Vertical") != 0) {
            runner.GetComponent<PlayerStateRunner>().SetState(MoveState);
        }
    }
    // Unity FixedUpdate
    public override void PhysicsUpdate() {
        DeAccel(new Vector3(rb.velocity.x,0f,rb.velocity.z));
    }
    public override void Exit() {
        Inputs.Player.GrabObject.performed -= OnGrab;
        // Inputs.Player.Dash.performed -= OnDash;
    }
    private void DeAccel(Vector3 Speed) {
        rb.AddForce(-Speed*DeAccelRate,ForceMode.Impulse);
    }
    void OnGrab(InputAction.CallbackContext context) {
        if (player.HeldObjects.Objects.Count > 0f) {
            player.HeldObjects.Objects[0].GetComponent<ThrowableObjects>().OnPickup();
            player.HeldObject = player.HeldObjects.Objects[0].GetComponent<ThrowableObjects>().ObjectToHold;
            runner.GetComponent<PlayerStateRunner>().SetState(HoldingObjectState);
        }
    }
    // void OnDash(InputAction.CallbackContext context) {
    //     runner.GetComponent<PlayerStateRunner>().SetState(DashState);
    // }
}
