using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;
    public string State;
    public float SwordAttackDistance;
    public Animator anim;
    public float swordAttackTime = 0.5f;
    private float timePassed = 0f;
    public float Speed;
    Rigidbody rb;
    private Vector3 Dir;
    [SerializeField] private float RotationAmount;
    [SerializeField] private float seeForwardRange;
    [SerializeField]LayerMask layerMask;
    [SerializeField] float AttackDrag;
    private bool started = false;
    [SerializeField] float detectionRange;
    float ogDrag;
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        ogDrag = rb.drag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate() {
        if (started) {
            Dir = PathFinding(transform.position,Player.position);
            if (State == "Follow" && Vector3.Distance(Player.position,transform.position) < SwordAttackDistance) {
                State = "Attack";
                timePassed = 0f;
                anim.Play("Attack");
                rb.drag = AttackDrag;
            }
            
            if (State == "Attack") {
                
                timePassed += Time.deltaTime;
                if (timePassed > swordAttackTime) {
                    State = "Follow";
                    anim.Play("Idle");
                    rb.drag = ogDrag;
                }
            }
            if (State == "Follow") {
                // rb.AddForce((Player.position-transform.position).normalized*Speed,ForceMode.Acceleration);
                rb.AddForce(Dir,ForceMode.Acceleration);
            }
        }
        else {
            if (Vector3.Distance(Player.position,transform.position) < detectionRange) {
                started = true;
            }
        }
    }

    Vector3 PathFinding(Vector3 ePos, Vector3 pPos) {
        Vector3 startingVector = pPos-ePos;
        
        float rotationDegrees = RotationAmount;
        List<Vector3> Vectors = new List<Vector3>();
        Vectors.Add(startingVector*10f);
        while (rotationDegrees < 360) {
            Vector3 dir = Quaternion.Euler(0,rotationDegrees,0)*startingVector;
            dir = new Vector3(dir.x,0,dir.z);
            // dir = dir*Vector3.Dot(dir,startingVector);
            float absoluteDifference = Mathf.Abs(-rotationDegrees);
            float distance = Mathf.Min(absoluteDifference, 360f - absoluteDifference);
            dir = dir*(Mathf.Lerp(0,30,(180-distance)/180));
            // Debug.Log(Mathf.Lerp(0,10,(180-distance)/180));
            Vectors.Add(dir);

            rotationDegrees+=RotationAmount;
        }
        Vector3 totalVector = Vector3.zero;
        for (int i = 0; i < Vectors.Count; i++) {
            Vector3 vector = Vectors[i];
            RaycastHit hit;
            Physics.Raycast(ePos, vector.normalized, out hit, seeForwardRange,layerMask);
            if (hit.collider != null) {
                Vectors[i] = Vector3.zero;
                // Debug.Log("hit");
                // totalVector+=(-vector/3);
            }
            // Debug.DrawRay(ePos, vector.normalized * seeForwardRange, Color.red);
            // Debug.DrawLine(ePos,new Vector3(ePos.x+Vectors[i].x,ePos.y+Vectors[i].y,ePos.z+Vectors[i].z),Color.white);
            totalVector=totalVector+Vectors[i];
        }
        totalVector = totalVector.normalized*Speed;
        // Debug.DrawLine(ePos,new Vector3(ePos.x+totalVector.x,ePos.y+totalVector.y,ePos.z+totalVector.z),Color.red);
        return totalVector;
    }
}
