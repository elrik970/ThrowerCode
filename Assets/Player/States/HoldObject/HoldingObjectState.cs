using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New HoldingObjectState", menuName = "States/Player/HoldingObjectState")]
public class HoldingObjectState : PlayerState<Player>
{

    // [SerializeField] Vector3 MovementVector;
    public GameObject HeldObject;
    [SerializeField] PlayerState<Player> IdleState;
    public float maxSpeed;
    public float OriginalMaxSpeed;
    public float DeAccelRate;
    private PlayerInputs Inputs;
    [SerializeField] float YOffset;
    [SerializeField] float ZOffset;

    [SerializeField] float HealAmount;
    [SerializeField] GameObject HealEffect;

    [SerializeField] float DividedBy;
    
    public override void Init(Player parent) {
        base.Init(parent);
        HeldObject = player.HeldObject;

        Inputs = player.Inputs;
        Inputs.Player.GrabObject.performed += OnThrow;
        Inputs.Player.Heal.performed += OnHeal;

        HeldObject = (GameObject)GameObject.Instantiate(HeldObject,new Vector3(0,YOffset,ZOffset),Quaternion.identity);
        HeldObject.transform.SetParent(player.ObjectHolder,false);

        maxSpeed = OriginalMaxSpeed-(HeldObject.GetComponent<Rigidbody>().mass/DividedBy);

        player.PickupSprite.enabled = false;
        player.ThrowSprite.enabled = true;
    }
    // Unity Update
    public override void ConstantUpdate() {
        player.RotateToCursor();
        HeldObject.transform.position = player.ObjectHolder.transform.position;
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
            DeAccel(new Vector3(rb.velocity.x,0f,rb.velocity.z));
        }
        else {
            Vector3 MovementVector = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical")).normalized;
            rb.velocity = new Vector3(MovementVector.x*maxSpeed,rb.velocity.y,MovementVector.z*maxSpeed);
        }
        
    }
    public override void Exit() {
        Inputs.Player.GrabObject.performed -= OnThrow;

        player.PickupSprite.enabled = true;
        player.ThrowSprite.enabled = false;

        Inputs.Player.Heal.performed -= OnHeal;
    }
    private void DeAccel(Vector3 Speed) {
        rb.AddForce(-Speed*DeAccelRate,ForceMode.Impulse);
    }
    void OnThrow(InputAction.CallbackContext context) {
        // Debug.Log("OMG");
        HeldObjectAbstractClass HeldObjectScript = HeldObject.GetComponent<HeldObject>();
        if (HeldObjectScript != null) {
            float YThrowForce = HeldObjectScript.YThrowForce;
            float ThrowForce = HeldObjectScript.ThrowForce;

            GameObject ThrownObject = (GameObject)GameObject.Instantiate(HeldObjectScript.ThrownObject,new Vector3(player.transform.position.x+player.transform.forward.x*2f,player.transform.position.y,player.transform.position.z+player.transform.forward.z*2f),Quaternion.Euler(HeldObjectScript.StartingRotation));

            Vector3 Throw = player.transform.forward*ThrowForce;

            ThrownObject.GetComponent<Rigidbody>().AddForce(new Vector3(Throw.x,YThrowForce,Throw.z),ForceMode.Impulse);

            Destroy(HeldObject.gameObject);

            runner.GetComponent<PlayerStateRunner>().SetState(IdleState);
        }
        else {
            UsedObjectClass UsedObject = HeldObject.GetComponent<UsedObjectClass>();

            UsedObject.OnUse();

            UsedObject.NumOfUsedTimes -= 1;

            if (UsedObject.NumOfUsedTimes <= 0) {
                Destroy(HeldObject.gameObject,0.5f);
                runner.GetComponent<PlayerStateRunner>().SetState(IdleState);
            }

        }
    }

    void OnHeal(InputAction.CallbackContext context) {
        Destroy(HeldObject.gameObject);

        player.Health.Health+=HealAmount;

        GameObject.Instantiate(HealEffect,player.transform.position,Quaternion.identity);

        runner.GetComponent<PlayerStateRunner>().SetState(IdleState);
    }

}