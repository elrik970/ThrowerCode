using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New MoveState", menuName = "States/Player/MoveState")]
public class MoveState : PlayerState<Player>
{

    // [SerializeField] Vector3 MovementVector;
    [SerializeField] PlayerState<Player> IdleState;
    [SerializeField] PlayerState<Player> HoldingObjectState;
    public PlayerState<Player> DashState;
    public float Accel;
    public float maxSpeed;
    private PlayerInputs Inputs;
    // private Rigidbody rb;
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

    }
    // Unity FixedUpdate
    public override void PhysicsUpdate() {
        if (Input.GetAxis("Horizontal") != 1&&Input.GetAxis("Horizontal") != -1&&Input.GetAxis("Vertical") != 1&&Input.GetAxis("Vertical") != -1) {
            runner.GetComponent<PlayerStateRunner>().SetState(IdleState);
        }
        else {
            Vector3 MovementVector = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical")).normalized;
            rb.velocity = new Vector3(MovementVector.x*maxSpeed,rb.velocity.y,MovementVector.z*maxSpeed);
        }
    }
    public override void Exit() {
        Inputs.Player.GrabObject.performed -= OnGrab;
        // Inputs.Player.Dash.performed -= OnDash;
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
