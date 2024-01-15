using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassDrawer : MonoBehaviour
{
    // Start is called before the first frame update
    public Mesh mesh;
    private Matrix4x4 matrix;
    public Vector3 scale;
    public Material material;
    private List<List<Matrix4x4>> Batches = new List<List<Matrix4x4>>(); 
    public Vector3 TopLeftPos;
    public float XArea;
    public float ZArea;
    public float Density;
    public Transform ParentGameObject;


    void Start()
    {
        TopLeftPos = transform.position;
        int AddedBlades = 0;
        float DensityInUnits = (1/Density);
        for (int j = 0; j < ZArea*Density;j++) {
            for (int i = 0; i < XArea*Density;i++) {
                if (AddedBlades < 1000&&AddedBlades != 0) {
                    Batches[Batches.Count-1].Add(Matrix4x4.TRS(new Vector3(TopLeftPos.x+i*DensityInUnits,TopLeftPos.y,TopLeftPos.z-j*DensityInUnits),Quaternion.identity,scale));
                    AddedBlades = 0;
                }
                if (AddedBlades == 0) {
                    Batches.Add(new List<Matrix4x4>());
                    AddedBlades = 1;
                }
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
