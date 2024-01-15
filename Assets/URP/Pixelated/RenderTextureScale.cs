using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureScale : MonoBehaviour
{
    // Start is called before the first frame update
    public float Scale;
    public RenderTexture Tex;
    void Start()
    {
        Tex.height = Mathf.RoundToInt(180*Scale);
        Tex.width = Mathf.RoundToInt(320*Scale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
