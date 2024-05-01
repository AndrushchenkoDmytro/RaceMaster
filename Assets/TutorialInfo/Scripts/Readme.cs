using System;
using UnityEngine;

public class Readme : ScriptableObject
{
    public Texture2D icon;
    public string title;
    public Section[] sections;
    public bool loadedLayout;

    [Serializable]
    public class Section
    {
        public string heading, text, linkText, url;
    }
}
/*
private Vector3 lastPoint = Vector3.zero;
private Vector3 newPoint;
private Vector3[] startVertexes;
private Vector3[] endVertexes;
private Vector3[] interpolatePoints;
private Vector3[] tempVertexes;
Vector3 spawnPos = Vector3.zero;
float newPointDir = 1;

private bool interpolateConoreTop = true;

private RoadMeshGenarator roadMeshGenarator;
[SerializeField] private Material material;
*/
/*
 
    private void Awake()
    {
        roadMeshGenarator = new RoadMeshGenarator(material);
        startVertexes = new Vector3[2];
        endVertexes = new Vector3[2];
        interpolatePoints = new Vector3[2];
        tempVertexes = new Vector3[2] { Vector3.left * roadWidth, Vector3.right * roadWidth};

        bool[] bools = new bool[pointsCout];
   
        int j = 2;
        for (int i = 0; i < pointsCout; i++)
        {
            if (j > 0)
            {
                bools[i] = true;
                j--;
            }
            else
            {
                bools[i] = false;
                j--;
                if (j == -2)
                {
                    j = 2;
                }
            }
        }
        j = 0;
        bool temp = bools[0];
        int roadPart = 1;
        for (int i = 0; i < pointsCout; i++)
        {
            if(roadPart == 0)
            {
                newPoint = GeneratePointForLineEnd(true);
                GameObject go = roadMeshGenarator.CreateHorizontalRoadLine(11, roadWidth, 12, "RoadLine");
                go.transform.position = spawnPos;
            }
            else
            {
                if (temp != bools[j])
                {
                    ChangeSpawnDir();
                }
                newPoint = GeneratePointForCurveEnd();
                startVertexes[0] = tempVertexes[0];
                startVertexes[1] = tempVertexes[1];

                if (bools[j])
                {
                    SetEndVertixesFromLeftToRight();
                }
                else
                {
                    SetEndVertixesFromRightToLeft();
                }

                temp = bools[j];
                GameObject go = roadMeshGenarator.CreateRoadCurve(in startVertexes, in endVertexes, in interpolatePoints, 20);
                go.transform.position = spawnPos;
                j++;
            }

            spawnPos += newPoint;

            roadPart = i % 2;
        }
    }

    private void SetEndVertixesFromLeftToRight()
    {
        if (interpolateConoreTop)
        {
            endVertexes[0] = new Vector3(newPoint.x, 0, newPoint.z + roadWidth);
            endVertexes[1] = new Vector3(newPoint.x, 0, newPoint.z - roadWidth);

            for (int j = 0; j < 2; j++)
            {
                interpolatePoints[j].x = startVertexes[j].x;
                interpolatePoints[j].z = endVertexes[j].z;
            }
            tempVertexes = new Vector3[] { Vector3.forward * roadWidth, Vector3.back * roadWidth };

        }
        else
        {
            endVertexes[0] = new Vector3(newPoint.x - roadWidth, 0, newPoint.z);
            endVertexes[1] = new Vector3(newPoint.x + roadWidth, 0, newPoint.z);

            for (int j = 0; j < 2; j++)
            {
                interpolatePoints[j].x = endVertexes[j].x;
                interpolatePoints[j].z = startVertexes[j].z;
            }
            tempVertexes = new Vector3[] { Vector3.left * roadWidth, Vector3.right * roadWidth };
        }
        interpolateConoreTop = !interpolateConoreTop;
    }

    private void SetEndVertixesFromRightToLeft()
    {
        if (interpolateConoreTop)
        {
            endVertexes[0] = new Vector3(newPoint.x, 0, newPoint.z - roadWidth);
            endVertexes[1] = new Vector3(newPoint.x, 0, newPoint.z + roadWidth);

            for (int j = 0; j < 2; j++)
            {
                interpolatePoints[j].x = startVertexes[j].x;
                interpolatePoints[j].z = endVertexes[j].z;
            }
            tempVertexes = new Vector3[] { Vector3.back * roadWidth, Vector3.forward * roadWidth };
        }
        else
        {
            endVertexes[0] = new Vector3(newPoint.x - roadWidth, 0, newPoint.z);
            endVertexes[1] = new Vector3(newPoint.x + roadWidth, 0, newPoint.z);

            for (int j = 0; j < 2; j++)
            {
                interpolatePoints[j].x = endVertexes[j].x;
                interpolatePoints[j].z = startVertexes[j].z;
            }
            tempVertexes = new Vector3[] { Vector3.left * roadWidth, Vector3.right * roadWidth };
        }
        interpolateConoreTop = !interpolateConoreTop;
    }

    private Vector3 GeneratePointForCurveEnd()
    {
        float percent = Random.Range(0.3f, 0.7f);
        float offset = Random.Range(minCurveLenth, maxCurveLenth);
        float x = offset * (1 - percent) * newPointDir;
        float z = offset * percent;

        return new Vector3(x, 0, z);
    }

    private Vector3 GeneratePointForLineEnd(bool horizontal)
    {
        float offset = Random.Range(minCurveLenth, maxCurveLenth);
        float x = 0;
        float z = 0;
        if (horizontal)
        {
            x = newPointDir * offset;
        }
        else
        {
            z = newPointDir * offset;
        }

        return new Vector3(x, 0, z);
    }

    private void ChangeSpawnDir()
    {
        newPointDir *= -1;
    
 */
