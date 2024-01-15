using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Snake : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public bool right;
    public bool left;
    public bool up;
    public bool down;
    public ParticleSystem LeftPs;
    public float MoveSpeed;
    public float JumpForce;
    public Rigidbody rb;
    private PlayerInputs Inputs;
    void Awake() {
        Inputs = new PlayerInputs();
        
        
    }
    void OnEnable() {
        Inputs.Enable();
    }

    void OnDisable() {
        if (Inputs != null) Inputs.Disable();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Inputs.Snake.ZPositive.performed += MoveUp;
        Inputs.Snake.ZNegative.performed += MoveDown;
        Inputs.Snake.XPositive.performed += MoveRight;
        Inputs.Snake.XNegative.performed += MoveLeft;
        Inputs.Snake.Jump.performed += OnJump;
    }
    void Update()
    {
        if (right) {
            rb.velocity = new Vector3(MoveSpeed,rb.velocity.y,rb.velocity.z);
        }
        if (left) {
            rb.velocity = new Vector3(-MoveSpeed,rb.velocity.y,rb.velocity.z);
        }
        else {
            // ParticleSystem.EmissionModule em = LeftPs.emission;
            // em.enabled = false;
        }
        if (up) {
            rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y,MoveSpeed);
        }
        if (down) {
            rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y,-MoveSpeed);
        }
        
    }
    void MoveRight(InputAction.CallbackContext context) {
        if (left) {
            left = false;
        }
        else {
            right = true;
            rb.velocity = new Vector3(MoveSpeed,rb.velocity.y,0f);
        }
        up = false;
        down = false;
    }
    void MoveLeft(InputAction.CallbackContext context) {
        if (right) {
            right = false;
        }
        else {
            left = true;
            rb.velocity = new Vector3(-MoveSpeed,rb.velocity.y,0f);
        }
        up = false;
        down = false;
    }
    void MoveUp(InputAction.CallbackContext context) {
        if (down) {
            down = false;
        }
        else {
            up = true;
            rb.velocity = new Vector3(0f,rb.velocity.y,MoveSpeed);
        }
        right = false;
        left = false;
        
    }
    void MoveDown(InputAction.CallbackContext context) {
        
        if (up) {
            up = false;
        }
        else {
            rb.velocity = new Vector3(0f,rb.velocity.y,-MoveSpeed);
            down = true;
        }
        right = false;
        left = false;
    }
    void OnJump(InputAction.CallbackContext context) {
        rb.AddForce(Vector3.up*JumpForce);
    }
}
