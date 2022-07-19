using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// check https://docs.unity3d.com/ScriptReference/Random.html
// "However, static state is problematic when working with threads or jobs... 
// ...  or if multiple independent random number generators are required."
using Random = System.Random;

public class GrassSpanner : MonoBehaviour
{

    public Texture2D heightMap;
    public float terrainHeight;
    public int terrainSize = 250;
    public Material terrainMat;
    public Material grassMat;
    private List<Vector3> verts = new List<Vector3>();
    private Random random;

    void Start()
    {
        this.random = new Random();
        GenerateTerrain();
        GenerateField(30, 50);        
    }

    private void GenerateTerrain()
    {
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();

        for (int i = 0; i < this.terrainSize; i++)
        {
            for (int j = 0; j < this.terrainSize; j++)
            {
                verts.Add(new Vector3(i, heightMap.GetPixel(i, j).grayscale * this.terrainHeight , j));
                if (i == 0 || j == 0)
                    continue;
                
                // I think I need to write this out step by step
                // is this actually drawing the positions of the vertices??
                tris.Add(terrainSize * i + j);
                tris.Add(terrainSize * i + j - 1);
                tris.Add(terrainSize * (i - 1) + j - 1);
                tris.Add(terrainSize * (i - 1) + j - 1);
                tris.Add(terrainSize * (i - 1) + j);
                tris.Add(terrainSize * i + j);
            }
        }

        // putting the vertices into an array to reference the UVs?
        Vector2[] uvs = new Vector2[verts.Count];
        // put them in that array
        for (var i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(verts[i].x, verts[i].z);
        }

        GameObject plane = new GameObject("groundPlane");
        plane.AddComponent<MeshFilter>();
        MeshRenderer renderer = plane.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = terrainMat;

        Mesh groundMesh = new Mesh();
        groundMesh.vertices = verts.ToArray(); // NativeArray.ToArray() - converts a "read only" array into a managed one.
        groundMesh.uv = uvs;
        groundMesh.triangles = tris.ToArray();
        groundMesh.RecalculateNormals();
        plane.GetComponent<MeshFilter>().mesh = groundMesh;

        // is this to clear a helper array out of memory when we're done generating?
        this.verts.Clear();
    }


    private void GenerateField(int grassPatchRowCount, int grassCountPerPatch)
    {
        List<int> indices = new List<int>();
        for (int i = 0; i < 65000; i++)
        {
            indices.Add(i);
        }

        Vector3 startPosition = new Vector3(0, 0, 0);
        Vector3 patchSize = new Vector3(terrainSize / grassPatchRowCount, 0, terrainSize / grassPatchRowCount);

        // ! this nested loop works because the terrain is square - be careful if adapting to rectangle
        // also consider changing the loop variable steps into u and v instead of x and y?
        for (int x = 0; x < grassPatchRowCount; x++)
        {
            for (int y = 0; y < grassPatchRowCount; y++)
            {
                this.GenerateGrass(startPosition, patchSize, grassPatchRowCount);
                startPosition.x += patchSize.x;
            }

            startPosition.x = 0;
            startPosition.z += patchSize.z;
        }

        GameObject grassLayer;
        MeshFilter mf;
        MeshRenderer renderer;
        Mesh m;

        // why refer to verts List here...
        // why loop it if it has a Count greater than 65k?
        while (verts.Count > 65000)
        {
            m = new Mesh();
            m.vertices = verts.GetRange(0, 65000).ToArray();
            // check https://docs.unity3d.com/ScriptReference/Mesh.SetIndices.html
            // takes the array of indices that define the mesh faces
            // and needs a definition of what kind of topology it has (i.e. triangles, quads, points etc.)
            // not too sure if final argument is necessary to negate optional arguments
            m.SetIndices(indices.ToArray(), MeshTopology.Points, 0);

            grassLayer = new GameObject("grassLayer");
            mf = grassLayer.AddComponent<MeshFilter>();
            renderer = grassLayer.AddComponent<MeshRenderer>();
            renderer.sharedMaterial = grassMat;
            mf.mesh = m;
            verts.RemoveRange(0, 65000);
        }

        m = new Mesh();
        m.vertices = verts.ToArray();
        m.SetIndices(indices.GetRange(0, verts.Count).ToArray(), MeshTopology.Points, 0);
        grassLayer = new GameObject("grassLayer");
        mf = grassLayer.AddComponent<MeshFilter>();
        renderer = grassLayer.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = grassMat;
        mf.mesh = m;

        return;
    }

    private void GenerateGrass(Vector3 startPosition, Vector3 patchSize, int grassCountPerPatch)
    {
        for(var i = 0; i < grassCountPerPatch; i++)
        {
            var randomizedZDistance = (float)this.random.NextDouble() * patchSize.z;
            var randomizedXDistance = (float)this.random.NextDouble() * patchSize.x;

            int indexX = (int)((startPosition.x + randomizedXDistance));
            int indexZ = (int)((startPosition.z + randomizedZDistance));

            if (indexX >= terrainSize)
            {
                indexX = (int)terrainSize - 1;
            }

            if (indexZ >= terrainSize)
            {
                indexZ = (int)terrainSize - 1;
            }

            var currentPosition = new Vector3(startPosition.x + (randomizedXDistance), heightMap.GetPixel(indexX, indexZ).grayscale * (terrainHeight + 1), startPosition.z + randomizedZDistance);
            this.verts.Add(currentPosition);
        }
    }

}
