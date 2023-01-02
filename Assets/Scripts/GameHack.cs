using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHack : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private EnemySpawner enemySpawner;
    Material lineMaterial;
    private Transform target;
    private SphereCollider targetCollider;

    private void Start()
    {
        GetReferences();
        CreateLineMaterial();
    }

    private void Update()
    {
        for (int i = 0; i < enemySpawner.enemyList.Count; i++)
        {
            if (enemySpawner.enemyList[i])
            {
                target = enemySpawner.enemyList[i].transform;
                targetCollider = target.GetComponent<SphereCollider>();
            }
        }
    }

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
        
        if(enemySpawner.enemyList[0])
        {
            Setup();
            
            Vector3 vecRadius = new Vector3(0f, targetCollider.radius, 0f);
            Vector3 entPos = cam.WorldToViewportPoint(enemySpawner.enemyList[0].transform.position);
            Vector3 entPos2D = cam.WorldToViewportPoint(target.position - vecRadius);
            Vector3 entHeadPos2D = cam.WorldToViewportPoint(target.position + vecRadius);
            if (entPos.z > 0)
            { 
                DrawLine(new Vector2(0.5f, 0f), entPos);
                DrawRect(entHeadPos2D, entPos2D);
            }
            
            GL.PopMatrix();
        }
   
    }

    void Setup()
    {
        GL.PushMatrix();
        lineMaterial.SetPass(0);
        GL.LoadOrtho();
    }

    void DrawLine(Vector2 start, Vector2 end)
    {
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex3(start.x, start.y, 0f);
        GL.Vertex3(end.x, end.y, 0f);
        GL.End();
    }

    

    void DrawRect(Vector2 top, Vector2 bot)
    {
        GL.Begin(GL.LINES);
        GL.Color(Color.red);

        float height = Mathf.Abs(top.y - bot.y);

        Vector2 tl, tr;
        tl.x = top.x - height / 4;
        tr.x = top.x + height / 4;
        tl.y = tr.y = top.y;

        Vector2 bl, br;
        bl.x = bot.x - height / 4;
        br.x = bot.x + height / 4;
        bl.y = br.y = bot.y;

        DrawLine(tl, tr);
        DrawLine(bl, br);
        DrawLine(tl, bl);
        DrawLine(tr, br);
    }

    private void GetReferences()
    {
        cam = GetComponentInChildren<Camera>();
    }
}
