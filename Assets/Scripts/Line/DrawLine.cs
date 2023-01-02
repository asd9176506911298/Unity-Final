
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{

    Color lineColor = Color.red;
    float distance = 100;
    Material lineMaterial;
    public Transform obj;


    void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            var shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    

    // cannot call this on update, line not visible then.. and OnPostRender() works on camera only
    void OnRenderObject()
    {
        CreateLineMaterial();
        GL.PushMatrix();
        lineMaterial.SetPass(0);
        GL.LoadOrtho();

        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex3(gameObject.GetComponent<Camera>().WorldToViewportPoint(obj.position).x, gameObject.GetComponent<Camera>().WorldToViewportPoint(obj.position).y, 0f);
        GL.Vertex3(0.5f, 0f, 0f);
        GL.End();

        GL.PopMatrix();
    }
}
