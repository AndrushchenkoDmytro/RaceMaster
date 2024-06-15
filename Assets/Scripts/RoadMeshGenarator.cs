using UnityEngine;

public class RoadMeshGenarator : MonoBehaviour 
{
    [SerializeField] private float roadWidth;
    [SerializeField] private float lineLenth;
    [SerializeField] private float maxCurveLenth;
    [SerializeField] private float minCurveLenth;
    [SerializeField] private int segmentsCount;

    private float curveLenth;
    public float GetLineLenth() { return lineLenth; }
    public float GetCurveLenth() { return curveLenth; }

    private GameObject topLine;
    private GameObject bottomLine;
    private GameObject rightLine;
    private GameObject leftLine;
    private GameObject curve1;
    private GameObject curve2;
    private GameObject curve3;
    private GameObject curve4;

    public GameObject GetTopLine()
    {
        return topLine;
    }
    public GameObject GetBottomLine()
    {
        return bottomLine;
    }
    public GameObject GetRightLine()
    {
        return rightLine;
    }

    public GameObject GetLeftLine() 
    {
        return leftLine;
    }
    public GameObject GetCurve1()
    {
        return curve1;
    }
    public GameObject GetCurve2()
    {    
        return curve2;
    }
    public GameObject GetCurve3()
    {
        return curve3;
    }
    public GameObject GetCurve4()
    {
        return curve4;
    }

    [SerializeField] public Material mat;

    void Awake()
    {
        curveLenth = Random.Range(minCurveLenth, maxCurveLenth);
        /*CreateTopLine();
        CreateBottomLine();
        CreatRightLine();
        CreatLeftlLine();
        CreateCurve1();
        CreateCurve2();
        CreateCurve3();
        CreateCurve4();*/
    }

    private void CreateTopLine()
    {
        GameObject roadLine = new GameObject();
        roadLine.name = "TopLine";
        MeshFilter mf = roadLine.AddComponent<MeshFilter>();
        MeshRenderer mr = roadLine.AddComponent<MeshRenderer>();
        mr.material = mat;
        Mesh roadMesh = new Mesh();

        // Set road vertixes positions
        Vector3[] startVertixes = new Vector3[2];
        startVertixes[0] = new Vector3(-roadWidth, 0, 0);
        startVertixes[1] = new Vector3(roadWidth, 0, 0);
        Vector3[] endVertixes = new Vector3[2];
        endVertixes[0] = new Vector3(-roadWidth, 0, lineLenth);
        endVertixes[1] = new Vector3(roadWidth, 0, lineLenth);

        //Form a set of vertices, uv, normals, triangles
        float segmentLenth = Vector3.Distance(startVertixes[0], endVertixes[0]) / segmentsCount;
        int vertCount = 2 + 2 * segmentsCount;
        Vector3[] vertices = new Vector3[vertCount];
        Vector3 lastPoint = startVertixes[0];
        Vector3 offset = Vector3.forward * segmentLenth;
        for (int i = 0; i < vertices.Length; i += 2)
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
        roadMesh.vertices = vertices;
        roadMesh.uv = FormMeshUV(vertCount);
        roadMesh.normals = FormMeshNormals(vertCount);
        roadMesh.triangles = FormMeshTriangles(segmentsCount);

        mf.mesh = roadMesh;
        topLine = roadLine;
        roadLine.SetActive(false);
    }
    private void CreateBottomLine()
    {
        GameObject roadLine = new GameObject();
        roadLine.name = "BottomLine";
        MeshFilter mf = roadLine.AddComponent<MeshFilter>();
        MeshRenderer mr = roadLine.AddComponent<MeshRenderer>();
        mr.material = mat;
        Mesh roadMesh = new Mesh();

        // Set road vertixes positions
        Vector3[] startVertixes = new Vector3[2];
        startVertixes[0] = new Vector3(-roadWidth, 0, -lineLenth);
        startVertixes[1] = new Vector3(roadWidth, 0, -lineLenth);
        Vector3[] endVertixes = new Vector3[2];
        endVertixes[0] = new Vector3(-roadWidth, 0, 0);
        endVertixes[1] = new Vector3(roadWidth, 0, 0);

        //Form a set of vertices, uv, normals, triangles
        float segmentLenth = Vector3.Distance(startVertixes[0], endVertixes[0]) / segmentsCount;
        int vertCount = 2 + 2 * segmentsCount;
        Vector3[] vertices = new Vector3[vertCount];
        Vector3 lastPoint = startVertixes[0];
        Vector3 offset = Vector3.forward * segmentLenth;
        for (int i = 0; i < vertices.Length; i += 2)
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
        roadMesh.vertices = vertices;
        roadMesh.uv = FormMeshUV(vertCount);
        roadMesh.normals = FormMeshNormals(vertCount);
        roadMesh.triangles = FormMeshTriangles(segmentsCount);

        mf.mesh = roadMesh;
        bottomLine = roadLine;
        roadLine.SetActive(false);
    }

    private void CreatRightLine()
    {
        GameObject roadLine = new GameObject();
        roadLine.name = "RightLine";
        MeshFilter mf = roadLine.AddComponent<MeshFilter>();
        MeshRenderer mr = roadLine.AddComponent<MeshRenderer>();
        mr.material = mat;
        Mesh roadMesh = new Mesh();

        // Set road vertixes positions
        Vector3[] startVertixes = new Vector3[2];
        startVertixes[0] = new Vector3(0, 0, roadWidth);
        startVertixes[1] = new Vector3(0, 0, -roadWidth);
        Vector3[] endVertixes = new Vector3[2];
        endVertixes[0] = new Vector3(lineLenth, 0, roadWidth);
        endVertixes[1] = new Vector3(lineLenth, 0,  -roadWidth);

        //Form a set of vertices, uv, normals, triangles
        float segmentLenth = Vector3.Distance(startVertixes[0], endVertixes[0]) / segmentsCount;
        int vertCount = 2 + 2 * segmentsCount;
        Vector3[] vertices = new Vector3[vertCount];
        Vector3 lastPoint = startVertixes[0];
        Vector3 offset = Vector3.right * segmentLenth;
        for (int i = 0; i < vertices.Length; i += 2)
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
        roadMesh.vertices = vertices;
        roadMesh.uv = FormMeshUV(vertCount);
        roadMesh.normals = FormMeshNormals(vertCount);
        roadMesh.triangles = FormMeshTriangles(segmentsCount);

        mf.mesh = roadMesh;
        rightLine = roadLine;
    }

    private void CreatLeftlLine()
    {
        GameObject roadLine = new GameObject();
        roadLine.name = "LeftlLine";
        MeshFilter mf = roadLine.AddComponent<MeshFilter>();
        MeshRenderer mr = roadLine.AddComponent<MeshRenderer>();
        mr.material = mat;
        Mesh roadMesh = new Mesh();

        // Set road vertixes positions
        Vector3[] startVertixes = new Vector3[2];
        startVertixes[0] = new Vector3(-lineLenth, 0, roadWidth);
        startVertixes[1] = new Vector3(-lineLenth, 0, -roadWidth);
        Vector3[] endVertixes = new Vector3[2];
        endVertixes[0] = new Vector3(0, 0, roadWidth);
        endVertixes[1] = new Vector3(0, 0, -roadWidth);

        //Form a set of vertices, uv, normals, triangles
        float segmentLenth = Vector3.Distance(startVertixes[0], endVertixes[0]) / segmentsCount;
        int vertCount = 2 + 2 * segmentsCount;
        Vector3[] vertices = new Vector3[vertCount];
        Vector3 lastPoint = startVertixes[0];
        Vector3 offset = Vector3.right * segmentLenth;
        for (int i = 0; i < vertices.Length; i += 2)
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
        roadMesh.vertices = vertices;
        roadMesh.uv = FormMeshUV(vertCount);
        roadMesh.normals = FormMeshNormals(vertCount);
        roadMesh.triangles = FormMeshTriangles(segmentsCount);

        mf.mesh = roadMesh;
        leftLine = roadLine;
    }
    private void CreateCurve1()    //   <-.
    {                              //     |
        GameObject roadCurve = new GameObject();
        roadCurve.name = "TopLeftCurve";
        MeshFilter mf = roadCurve.AddComponent<MeshFilter>();
        MeshRenderer mr = roadCurve.AddComponent<MeshRenderer>();
        mr.material = mat;
        Mesh roadMesh = new Mesh();

        // Set road vertixes positions
        Vector3[] startVertixes = new Vector3[2];
        startVertixes[0] = new Vector3(-roadWidth, 0, 0);
        startVertixes[1] = new Vector3(roadWidth, 0, 0);
        Vector3[] endVertixes = new Vector3[2];
        endVertixes[0] = new Vector3(startVertixes[0].x - curveLenth, 0, curveLenth - roadWidth);
        endVertixes[1] = new Vector3(endVertixes[0].x, 0, curveLenth + roadWidth);
        Vector3[] interpolatePoints = new Vector3[2];

        interpolatePoints[0] = new Vector3(startVertixes[0].x, 0, endVertixes[0].z);
        interpolatePoints[1] = new Vector3(startVertixes[1].x, 0, endVertixes[1].z);

        float segmentLenth = 1f / segmentsCount;
        int vertCount = 2 + 2 * segmentsCount;
        Vector3[] vertices = new Vector3[vertCount];
        int v = vertCount / 2;
        for (int i = 0, k = 0; k < v; i += 2, k++)
        {
            vertices[i] = QuadroLerp(startVertixes[0], interpolatePoints[0], endVertixes[0], segmentLenth * k);
        }

        for (int i = 1, k = 0; k < v; i += 2, k++)
        {
            vertices[i] = QuadroLerp(startVertixes[1], interpolatePoints[1], endVertixes[1], segmentLenth * k);
        }

        roadMesh.vertices = vertices;
        roadMesh.uv = FormMeshUV(vertCount);
        roadMesh.normals = FormMeshNormals(vertCount);
        roadMesh.triangles = FormMeshTriangles(segmentsCount);
        roadMesh.RecalculateBounds();
        mf.mesh = roadMesh;

        curve1 = roadCurve;
    }

    private void CreateCurve2()     //  .->
    {                               //  |
        GameObject roadCurve = new GameObject();
        roadCurve.name = "TopRightCurve";
        MeshFilter mf = roadCurve.AddComponent<MeshFilter>();
        MeshRenderer mr = roadCurve.AddComponent<MeshRenderer>();
        mr.material = mat;
        Mesh roadMesh = new Mesh();

        // Set road vertixes positions
        Vector3[] startVertixes = new Vector3[2];
        startVertixes[0] = new Vector3(-roadWidth, 0, 0);
        startVertixes[1] = new Vector3(roadWidth, 0, 0);
        Vector3[] endVertixes = new Vector3[2];
        endVertixes[0] = new Vector3(startVertixes[1].x + curveLenth, 0, curveLenth + roadWidth);
        endVertixes[1] = new Vector3(endVertixes[0].x, 0, curveLenth - roadWidth);
        Vector3[] interpolatePoints = new Vector3[2];

        interpolatePoints[0] = new Vector3(startVertixes[0].x, 0, endVertixes[0].z);
        interpolatePoints[1] = new Vector3(startVertixes[1].x, 0, endVertixes[1].z);

        float segmentLenth = 1f / segmentsCount;
        int vertCount = 2 + 2 * segmentsCount;
        Vector3[] vertices = new Vector3[vertCount];
        int v = vertCount / 2;
        for (int i = 0, k = 0; k < v; i += 2, k++)
        {
            vertices[i] = QuadroLerp(startVertixes[0], interpolatePoints[0], endVertixes[0], segmentLenth * k);
        }

        for (int i = 1, k = 0; k < v; i += 2, k++)
        {
            vertices[i] = QuadroLerp(startVertixes[1], interpolatePoints[1], endVertixes[1], segmentLenth * k);
        }

        roadMesh.vertices = vertices;
        roadMesh.uv = FormMeshUV(vertCount);
        roadMesh.normals = FormMeshNormals(vertCount);
        roadMesh.triangles = FormMeshTriangles(segmentsCount);
        roadMesh.RecalculateBounds();
        mf.mesh = roadMesh;

        curve2 = roadCurve;
    }

    private void CreateCurve3()     //  |
    {                               //  *->
        GameObject roadCurve = new GameObject();
        roadCurve.name = "BottomRighttCurve";
        MeshFilter mf = roadCurve.AddComponent<MeshFilter>();
        MeshRenderer mr = roadCurve.AddComponent<MeshRenderer>();
        mr.material = mat;
        Mesh roadMesh = new Mesh();

        // Set road vertixes positions
        Vector3[] startVertixes = new Vector3[2];
        startVertixes[0] = new Vector3(0, 0, -roadWidth);
        startVertixes[1] = new Vector3(0, 0, roadWidth);

        Vector3[] endVertixes = new Vector3[2];
        endVertixes[0] = new Vector3(-curveLenth - roadWidth, 0, curveLenth);
        endVertixes[1] = new Vector3(-curveLenth + roadWidth, 0, curveLenth);

        Vector3[] interpolatePoints = new Vector3[2];
        interpolatePoints[0] = new Vector3(endVertixes[0].x, 0, startVertixes[0].z);
        interpolatePoints[1] = new Vector3(endVertixes[1].x, 0, startVertixes[1].z);

        float segmentLenth = 1f / segmentsCount;
        int vertCount = 2 + 2 * segmentsCount;
        Vector3[] vertices = new Vector3[vertCount];
        int v = vertCount / 2;
        for (int i = 0, k = 0; k < v; i += 2, k++)
        {
            vertices[i] = QuadroLerp(startVertixes[0], interpolatePoints[0], endVertixes[0], segmentLenth * k);
        }

        for (int i = 1, k = 0; k < v; i += 2, k++)
        {
            vertices[i] = QuadroLerp(startVertixes[1], interpolatePoints[1], endVertixes[1], segmentLenth * k);
        }

        roadMesh.vertices = vertices;
        roadMesh.uv = FormMeshUV(vertCount);
        roadMesh.normals = FormMeshNormals(vertCount);
        roadMesh.triangles = FormMeshTriangles(segmentsCount);
        roadMesh.RecalculateBounds();
        mf.mesh = roadMesh;

        curve3 = roadCurve;
    }

    private void CreateCurve4()     //    |
    {                               //  <-*
        GameObject roadCurve = new GameObject();
        roadCurve.name = "BottomLeftCurve";
        MeshFilter mf = roadCurve.AddComponent<MeshFilter>();
        MeshRenderer mr = roadCurve.AddComponent<MeshRenderer>();
        mr.material = mat;
        Mesh roadMesh = new Mesh();

        // Set road vertixes positions
        Vector3[] startVertixes = new Vector3[2];
        startVertixes[0] = new Vector3(0, 0,  roadWidth);
        startVertixes[1] = new Vector3(0, 0, -roadWidth);

        Vector3[] endVertixes = new Vector3[2];
        endVertixes[0] = new Vector3(curveLenth - roadWidth, 0, curveLenth);
        endVertixes[1] = new Vector3(curveLenth + roadWidth, 0, curveLenth);

        Vector3[] interpolatePoints = new Vector3[2];
        interpolatePoints[0] = new Vector3(endVertixes[0].x, 0, startVertixes[0].z);
        interpolatePoints[1] = new Vector3(endVertixes[1].x, 0, startVertixes[1].z);

        float segmentLenth = 1f / segmentsCount;
        int vertCount = 2 + 2 * segmentsCount;
        Vector3[] vertices = new Vector3[vertCount];
        int v = vertCount / 2;
        for (int i = 0, k = 0; k < v; i += 2, k++)
        {
            vertices[i] = QuadroLerp(startVertixes[0], interpolatePoints[0], endVertixes[0], segmentLenth * k);
        }

        for (int i = 1, k = 0; k < v; i += 2, k++)
        {
            vertices[i] = QuadroLerp(startVertixes[1], interpolatePoints[1], endVertixes[1], segmentLenth * k);
        }

        roadMesh.vertices = vertices;
        roadMesh.uv = FormMeshUV(vertCount);
        roadMesh.normals = FormMeshNormals(vertCount);
        roadMesh.triangles = FormMeshTriangles(segmentsCount);
        roadMesh.RecalculateBounds();
        mf.mesh = roadMesh;

        curve4 = roadCurve;
    }


    public GameObject CreateRoadLine(in Vector3[] startVertixes, in Vector3[] endVertixes, int segmentsCount, string name = "RoadLine")
    {
        GameObject roadLine = new GameObject();
        roadLine.name = name;
        MeshFilter mf = roadLine.AddComponent<MeshFilter>();
        MeshRenderer mr = roadLine.AddComponent<MeshRenderer>();
        BoxCollider bc = roadLine.AddComponent<BoxCollider>();
        mr.material = mat;

        float segmentLenth = Vector3.Distance(startVertixes[0], endVertixes[0]) / segmentsCount;
        int vertCount = 2 + 2 * segmentsCount;
        Vector3[] vertices = new Vector3[vertCount];
        Vector3 lastPoint = startVertixes[0];
        Vector3 offset;
        if (startVertixes[0].x == endVertixes[0].x)
        {
            offset = Vector3.forward * segmentLenth;
            bc.center = new Vector3(startVertixes[0].x + roadWidth, 0, endVertixes[0].z - lineLenth * 0.5f);
            bc.size = new Vector3(roadWidth * 2, 0, lineLenth);
        }
        else
        {
            if(startVertixes[0].x > endVertixes[1].x) // left
            {
                offset = Vector3.left * segmentLenth;
                bc.center = new Vector3(endVertixes[0].x + lineLenth * 0.5f, 0, startVertixes[0].z + roadWidth);
                bc.size = new Vector3(lineLenth, 0, roadWidth * 2);
            }
            else
            {
                offset = Vector3.right * segmentLenth;
                bc.center = new Vector3(endVertixes[0].x - lineLenth * 0.5f, 0, startVertixes[0].z - roadWidth);
                bc.size = new Vector3(lineLenth, 0, roadWidth * 2);
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

        Mesh roadMesh = new Mesh();
        roadMesh.vertices = vertices;
        roadMesh.uv = FormMeshUV(vertCount);
        roadMesh.normals = FormMeshNormals(vertCount);
        roadMesh.triangles = FormMeshTriangles(segmentsCount);

        mf.mesh = roadMesh;

        return roadLine;
    }
    public GameObject CreateRoadCurve(in Vector3[] startVertexes, in Vector3[] endVertexes, in Vector3[] interpolatePoints, int segmentsCount, CurveDirection direction, int index, string name = "RoadCurve")
    {
        GameObject roadCurve = new GameObject();
        roadCurve.name = name;
        MeshFilter mf = roadCurve.AddComponent<MeshFilter>();
        MeshRenderer mr = roadCurve.AddComponent<MeshRenderer>();
        MeshCollider mc = roadCurve.AddComponent <MeshCollider>();
        BoxCollider bc = roadCurve.AddComponent<BoxCollider>();
        CurveTrigger curveTrigger = roadCurve.AddComponent<CurveTrigger>();
        curveTrigger.SetCurveValues(startVertexes, endVertexes, interpolatePoints, direction,index);
        mr.material = mat;



        if (direction == CurveDirection.topLeft)
        {
            bc.center = new Vector3((startVertexes[1].x + endVertexes[1].x) * 0.5f, 0, (startVertexes[1].z + endVertexes[1].z) * 0.5f);
            bc.size = new Vector3(Mathf.Abs(startVertexes[1].x - endVertexes[1].x), 1, Mathf.Abs(startVertexes[1].z - endVertexes[1].z));
        }
        else if(direction == CurveDirection.topRight)
        {
            bc.center = new Vector3((startVertexes[0].x + endVertexes[0].x) * 0.5f, 0, (startVertexes[0].z + endVertexes[0].z) * 0.5f);
            bc.size = new Vector3(Mathf.Abs(endVertexes[0].x - startVertexes[0].x), 1, Mathf.Abs(startVertexes[0].z - endVertexes[0].z));
        }
        else if(direction == CurveDirection.bottomLeft)
        {
            bc.center = new Vector3((startVertexes[1].x + endVertexes[1].x) * 0.5f, 0, (startVertexes[1].z + endVertexes[1].z) * 0.5f);
            bc.size = new Vector3(Mathf.Abs(startVertexes[1].x - endVertexes[1].x), 1, Mathf.Abs(startVertexes[1].z - endVertexes[1].z));
        }
        else
        {
            bc.center = new Vector3((startVertexes[0].x + endVertexes[0].x) * 0.5f, 0, (startVertexes[0].z + endVertexes[0].z) * 0.5f);
            bc.size = new Vector3(Mathf.Abs(endVertexes[0].x - startVertexes[0].x), 1, Mathf.Abs(startVertexes[0].z - endVertexes[0].z));
        }
        bc.isTrigger = true;

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

        Mesh roadMesh = new Mesh();
        roadMesh.vertices = vertices;
        roadMesh.uv = FormMeshUV(vertCount);
        roadMesh.normals = FormMeshNormals(vertCount);
        roadMesh.triangles = FormMeshTriangles(segmentsCount);
        roadMesh.RecalculateBounds();
        mf.mesh = roadMesh;
        mc.sharedMesh = roadMesh;
        
        return roadCurve;

    }


    public static Vector3 QuadroLerp(Vector3 a, Vector3 b, Vector3 c, float t)
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

    private Vector2[] FormMeshUV(int vertCount)
    {

        Vector2[] uv = new Vector2[vertCount];
        Vector2[] texturePoints = new Vector2[] { new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0f, 1f), new Vector2(1f, 1f) };
        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = texturePoints[i % 4];
        }
        return uv;
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
