using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturePaintingManager : MonoBehaviour
{
    public static TexturePaintingManager Instance = null;
    public Renderer rend;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

  
    void Update()
     {
            if (!Input.GetMouseButton(0))
                return;

            RaycastHit hit;
            if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
                return;

            Renderer rend = hit.transform.GetComponent<Renderer>();
            MeshCollider meshCollider = hit.collider as MeshCollider;

            if (rend == null || rend.sharedMaterial == null || meshCollider == null)
                return;

            Texture2D tex = rend.material.GetTexture("_Mask_Texture") as Texture2D;
            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= tex.width;
            pixelUV.y *= tex.height;

            PaintSquare(2,2,tex.width,tex.height,Color.white,tex);
     }
    
    public void PaintSquare(int x, int y, int w, int h, Color c,Texture2D texture)
    {
        Debug.Log("Painting at " + x + ","+y );
        Color[] carray = new Color[w * h];
        for(int i = 0; i<carray.Length;i++) { carray[i] = c; }
        texture.SetPixels(x, y, w, h, carray);
    }
}
