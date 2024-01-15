using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCubes : MonoBehaviour
{
    // Start is called before the first frame update
    public Mesh mesh;
    private Matrix4x4 matrix;
    public Vector3 scale;
    public Material material;
    private List<List<Matrix4x4>> Batches = new List<List<Matrix4x4>>(); 
    public float YSize;
    public float NoCubeYSpot;
    public float XSize;
    

    public float maxScale;
    public float minScale;

    public float Amount;


    void Start()
    {
        int AddedBlades = 0;
        for (int i = 0; i < Amount;i++) {
            if (AddedBlades < 1000&&AddedBlades != 0) {
                float randomScale = Random.Range(minScale,maxScale); 
                float YSpot = 0f;
                if (Random.Range(0,1) == 1) {
                    YSpot = Random.Range(NoCubeYSpot/2,YSize/2);
                }
                else {
                    YSpot = Random.Range(-YSize/2,-NoCubeYSpot/2);
                }
                Batches[Batches.Count-1].Add(Matrix4x4.TRS(new Vector3(Random.Range(-XSize/2,XSize/2),YSpot,Random.Range(-XSize/2,XSize/2)),Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)),new Vector3(randomScale,randomScale,randomScale)));
                AddedBlades = 0;
            }
            if (AddedBlades == 0) {
                Batches.Add(new List<Matrix4x4>());
                AddedBlades = 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RenderBatches();   
    }
    void RenderBatches() {
        foreach (List<Matrix4x4> Batch in Batches) {
            Graphics.DrawMeshInstanced(mesh,0,material,Batch);
        }
    }

}
