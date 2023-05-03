using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMap : MonoBehaviour
{
    public Renderer texutreRenderer;

    public void DrawTexture(Texture2D texture)
    { 
        texutreRenderer.sharedMaterial.mainTexture = texture;
        texutreRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
}
