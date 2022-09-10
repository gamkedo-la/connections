using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll_HDRP_Material : MonoBehaviour
{
    public int materialIndex = 0;
    public Vector2 uvAnimationRate = new Vector2( 1.0f, 0.0f );
    public string textureName = "_BaseColorMap";
    public Renderer render;
    Vector2 uvOffset = Vector2.zero;
    void Update()
    {
        uvOffset = ( uvAnimationRate * Time.time );
        if( render.enabled )
        {
            render.materials[ materialIndex ].SetTextureOffset( textureName, uvOffset );
        }
    }
}
