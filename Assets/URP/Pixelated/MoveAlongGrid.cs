using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongGrid : MonoBehaviour
{
    // Start is called before the first frame update
    private float PPU;
    private Vector3 vectorInPixels;
    public float OrthoSize;
    public Transform parent;
    public RenderTexture Tex;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(transform.position);
    }
    void LateUpdate() {
        PPU = Tex.height/OrthoSize*2;

        vectorInPixels = new Vector3(Mathf.RoundToInt(parent.transform.position.x*PPU),Mathf.RoundToInt(parent.transform.position.y*PPU),Mathf.RoundToInt(parent.transform.position.z*PPU));
        transform.position = vectorInPixels / PPU;
    }
}
