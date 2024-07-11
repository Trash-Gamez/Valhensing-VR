using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartycleCollisionPainter : MonoBehaviour
{
    private ParticleSystem ps;
    private List<ParticleCollisionEvent> collisionEvents;
 
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag("Paintable")) return;
        int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);
        int i = 0;

        while (i < numCollisionEvents)
        {
            RaycastHit hit = new RaycastHit();
            float rayLength = 0.1f;
            Ray ray = new Ray(collisionEvents[i].intersection - collisionEvents[i].normal * rayLength * 0.5f, collisionEvents[i].normal);

            Texture2D maskTexture = GetTexture(other);
            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= maskTexture.width;
            pixelUV.y *= maskTexture.height;

            TexturePaintingManager.Instance.PaintSquare(System.Convert.ToInt32(pixelUV.x), System.Convert.ToInt32(pixelUV.y), 20, 20, Color.white, maskTexture);
             
            i++;
        }
    }

    private Texture2D GetTexture(GameObject o)
    {
        Texture2D tex = o.GetComponent<Renderer>().material.GetTexture("_Mask_Texture") as Texture2D;
        return tex;
    }
}
