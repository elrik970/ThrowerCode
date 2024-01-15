using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRunner : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerState<Player> curState;
    void Start()
    {
        curState.Init(GetComponent<Player>());
    }

    // Update is called once per frame
    void Update()
    {
        curState.ChangeState();
        curState.CaptureInputs();
        curState.ConstantUpdate();
    }
    void FixedUpdate() 
    {
        curState.PhysicsUpdate();
    }
    public void SetState(PlayerState<Player> stateToChangeTo) {
        if (curState != null) {
            curState.Exit();
        }
        curState = stateToChangeTo;
        stateToChangeTo.Init(GetComponent<Player>());
    }
}
