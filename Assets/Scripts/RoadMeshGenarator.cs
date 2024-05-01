using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoadMeshGenarator: MonoBehaviour
{
    [SerializeField] public Material mat;
    public GameObject CreateRoadLine(in Vector3[] startVertixes, in Vector3[] endVertixes, int segmentsCount, string name = "RoadLine")
    {
        GameObject roadLine = new GameObject();
        roadLine.name = name;
        MeshFilter mf = roadLine.AddComponent<MeshFilter>();
        MeshRenderer mr = roadLine.AddComponent<MeshRenderer>();
        mr.material = mat;
        Mesh roadMesh = new Mesh();

        float segmentLenth = Vector3.Distance(startVertixes[0], endVertixes[0]) / segmentsCount;
        int vertCount = 2 + 2 * segmentsCount;
        Vector3[] vertices = new Vector3[vertCount];
        Vector3 lastPoint = startVertixes[0];
        Vector3 offset;
        if (startVertixes[0].x == endVertixes[0].x)
        {
            offset = Vector3.forward * segmentLenth;
        }
        else
        {
            if(startVertixes[0].x > endVertixes[1].x) // left
            {
                offset = Vector3.left * segmentLenth;
            }
            else
            {
                offset = Vector3.right * segmentLenth;
            }
        }

        for (int i = 0; i < vertices.Length; i+=2)
        {
            vertices[i] = lastPoint;
            lastPoint += offset;
        }
        
        lastPoint = startVertixes[1];
        for (int i = 1; i < vertices.Length; i += 2)
        {
            vertices[i] = lastPoint;
            lastPoint += offset;
        }

        Vector2[] uv = new Vector2[vertCount];
        Vector2[] texturePoints = new Vector2[] { new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0f, 1f), new Vector2(1f, 1f) };
        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = texturePoints[i % 4];
        }

        roadMesh.vertices = vertices;
        roadMesh.uv = uv;
        roadMesh.normals = FormMeshNormals(vertCount);
        roadMesh.triangles = FormMeshTriangles(segmentsCount);

        mf.mesh = roadMesh;

        return roadLine;
    }



    public GameObject CreateRoadCurve(in Vector3[] startVertexes, in Vector3[] endVertexes, in Vector3[] interpolatePoints, int segmentsCount, string name = "RoadCurve")
    {
        GameObject roadCurve = new GameObject();
        roadCurve.name = name;
        MeshFilter mf = roadCurve.AddComponent<MeshFilter>();
        MeshRenderer mr = roadCurve.AddComponent<MeshRenderer>();
        mr.material = mat;
        Mesh roadMesh = new Mesh();

        float segmentLenth = 1f / segmentsCount;
        int vertCount = 2 + 2 * segmentsCount;
        Vector3[] vertices = new Vector3[vertCount];
        int v = vertCount / 2;
        for (int i = 0, k = 0; k < v; i += 2, k++)
        {
            vertices[i] = QuadroLerp(startVertexes[0], interpolatePoints[0], endVertexes[0],segmentLenth * k);
        }

        for (int i = 1, k = 0; k < v; i += 2, k++)
        {
            vertices[i] = QuadroLerp(startVertexes[1], interpolatePoints[1], endVertexes[1], segmentLenth * k);
        }

        Vector2[] uv = new Vector2[vertCount];
        Vector2[] texturePoints = new Vector2[] { new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0f, 1f), new Vector2(1f, 1f) };
        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = texturePoints[i % 4];
        }

        roadMesh.vertices = vertices;
        roadMesh.uv = uv;
        roadMesh.normals = FormMeshNormals(vertCount);
        roadMesh.triangles = FormMeshTriangles(segmentsCount);
        roadMesh.RecalculateBounds();
        mf.mesh = roadMesh;
        return roadCurve;

    }

    private Vector3 QuadroLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        Vector3 ac = Vector3.Lerp(ab, bc, t);
        return ac;
    }

    private int[] FormMeshTriangles(int segmentsCount)
    {
        int[] triangles = new int[segmentsCount * 2 * 3];
        int j = 0;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            if (j % 2 == 0)
            {
                triangles[i] = j;
                triangles[i + 1] = j + 2;
                triangles[i + 2] = j + 1;
                j++;
            }
            else
            {
                triangles[i] = j;
                triangles[i + 1] = j + 1;
                triangles[i + 2] = j + 2;
                j++;
            }
        }
        return triangles;
    }

    private Vector3[] FormMeshNormals(int vertCount)
    {
        Vector3[] normals = new Vector3[vertCount];
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = Vector3.back;
        }

        return normals;
    }
}
