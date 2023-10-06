using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinMeshToMeshRenderer : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;
    [SerializeField]private MeshFilter meshFilter;
    [SerializeField] private Mesh meshRenderer;
    [SerializeField] private float refresRate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateMesh());
    }


    IEnumerator UpdateMesh()
    {
        while (gameObject.activeSelf)
        {
            Mesh m = new Mesh();
            skinnedMesh.BakeMesh(m);
          
            meshFilter.mesh = m;
            yield return new WaitForSeconds(refresRate);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
