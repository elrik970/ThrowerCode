using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera Camera;
    public float RaycastDistance;
    public Transform Box;


    public HeldObjects HeldObjects;
    public Transform ObjectHolder;

    public GameObject HeldObject;

    public PlayerInputs Inputs;

    public LayerMask RotateToCursorLayerMask;

    public SpriteRenderer PickupSprite;
    public SpriteRenderer ThrowSprite;

    public HealthClass Health;

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
        Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RotateToCursor() {
        Ray ray = Camera.ViewportPointToRay(new Vector3(Input.mousePosition.x/Screen.width,Input.mousePosition.y/Screen.height,0));
        
        RaycastHit hit; 
        if (Physics.Raycast(ray, out hit,100,RotateToCursorLayerMask)) {
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
            Debug.DrawLine(ray.origin,hit.point,Color.white);
            // Debug.Log(hit.collider.gameObject.layer);
            transform.forward = hit.point-transform.position;
            
            Quaternion originalRotation = transform.rotation;

            // Set the X and Z components of the rotation to zero
            Vector3 zeroedRotation = new Vector3(0f, transform.rotation.eulerAngles.y, 0f);

            // Assign the new rotation with X and Z components set to zero
            transform.rotation = Quaternion.Euler(zeroedRotation);
        }
        
    }
    
}
