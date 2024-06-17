using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class RoadPointsGenerator : MonoBehaviour
{
    public static RoadPointsGenerator instatnce;

    public float GetRoadWidth()
    {
        return roadWidth;
    }
    public float GetLineLenth()
    {
        return lineLenth;
    }

    [SerializeField] private RoadMeshGenarator generator;
    [SerializeField] private float roadWidth;
    [SerializeField] private float lineLenth;
    [SerializeField] private float maxCurveLenth;
    [SerializeField] private float minCurveLenth;
    [SerializeField] private int segmentsCount;

    RoadSegmentType dir = RoadSegmentType.line;
    int duplicationCount = 0;

    public RoadSegment[] roadSegments;

    public void Awake()
    {
        if(instatnce == null)
        {
            instatnce = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Initialize();
        for (int i = 0; i < segmentsCount; i++)
        {
            roadSegments[i].type = dir;
            if (dir == RoadSegmentType.line)
            {
                if(duplicationCount < 3)
                {
                    duplicationCount++;
                }
                else
                {
                    dir = RoadSegmentType.curve;
                    duplicationCount = 0;
                }
            }
            else if(dir == RoadSegmentType.curve)
            {
                int c = Random.Range(0,3);
                if(c == 0)
                {
                    dir = RoadSegmentType.curve;
                }
                else
                {
                    dir = RoadSegmentType.line;
                }
            }
        }

        for (int i = 0; i < segmentsCount; i++)
        {
            if (roadSegments[i].type == RoadSegmentType.line)
            {
                if(roadSegments[i].startPoints[0].x != roadSegments[i].startPoints[1].x)
                {
                    roadSegments[i].lastPoints[0] = roadSegments[i].startPoints[0] + Vector3.forward * lineLenth;
                    roadSegments[i].lastPoints[1] = roadSegments[i].startPoints[1] + Vector3.forward * lineLenth;
                }
                else
                {
                    if (roadSegments[i].startPoints[0].x < roadSegments[i - 1].startPoints[0].x)
                    {
                        roadSegments[i].lastPoints[0] = roadSegments[i].startPoints[0] + Vector3.left * lineLenth;
                        roadSegments[i].lastPoints[1] = roadSegments[i].startPoints[1] + Vector3.left * lineLenth;
                    }
                    else
                    {
                        roadSegments[i].lastPoints[0] = roadSegments[i].startPoints[0] + Vector3.right * lineLenth;
                        roadSegments[i].lastPoints[1] = roadSegments[i].startPoints[1] + Vector3.right * lineLenth;
                    }
                }
                generator.CreateRoadLine(in roadSegments[i].startPoints, in roadSegments[i].lastPoints, 14);
            }
            else
            {
                CurveDirection curveDirection;
                if (roadSegments[i].startPoints[0].x != roadSegments[i].startPoints[1].x)
                {
                    Vector3 endPoint = new Vector3(Random.Range(5,12),0,Random.Range(7,16)); 
                    int r = Random.Range(0,101);
                    if(r <= 50) // left
                    {
                        endPoint.x *= -1;
                        roadSegments[i].lastPoints[0] = roadSegments[i].startPoints[0] + new Vector3(endPoint.x, 0, endPoint.z - roadWidth);
                        roadSegments[i].lastPoints[1] = roadSegments[i].startPoints[1] + new Vector3(0, 0, endPoint.z + roadWidth);
                        roadSegments[i].lastPoints[1].x = roadSegments[i].lastPoints[0].x;
                        curveDirection = CurveDirection.topLeft;
                    }
                    else
                    {
                        roadSegments[i].lastPoints[0] = roadSegments[i].startPoints[0] + new Vector3(0, 0, endPoint.z + roadWidth);
                        roadSegments[i].lastPoints[1] = roadSegments[i].startPoints[1] + new Vector3(endPoint.x, 0, endPoint.z - roadWidth);
                        roadSegments[i].lastPoints[0].x = roadSegments[i].lastPoints[1].x;
                        curveDirection = CurveDirection.topRight;

                    }

                    roadSegments[i].interpolatePoints[0] = new Vector3(roadSegments[i].startPoints[0].x, 0, roadSegments[i].lastPoints[0].z);
                    roadSegments[i].interpolatePoints[1] = new Vector3(roadSegments[i].startPoints[1].x, 0, roadSegments[i].lastPoints[1].z);
                    generator.CreateRoadCurve(in roadSegments[i].startPoints, in roadSegments[i].lastPoints, in roadSegments[i].interpolatePoints, 20, curveDirection,i);
                }
                else
                {
                    Vector3 endPoint = new Vector3(Random.Range(7, 16), 0, Random.Range(7, 14));

                    if (roadSegments[i].startPoints[0].x < roadSegments[i-1].startPoints[0].x) // left
                    {
                        endPoint.x *= -1;

                        roadSegments[i].lastPoints[0] = roadSegments[i].startPoints[0] + new Vector3(endPoint.x - roadWidth, 0, endPoint.z);
                        roadSegments[i].lastPoints[1] = roadSegments[i].startPoints[1] + new Vector3(endPoint.x + roadWidth, 0, endPoint.z);
                        roadSegments[i].lastPoints[0].z = roadSegments[i].lastPoints[1].z;

                        roadSegments[i].interpolatePoints[0] = new Vector3(roadSegments[i].lastPoints[0].x, 0, roadSegments[i].startPoints[0].z);
                        roadSegments[i].interpolatePoints[1] = new Vector3(roadSegments[i].lastPoints[1].x, 0, roadSegments[i].startPoints[1].z);
                        curveDirection = CurveDirection.bottomRight;
                    }
                    else
                    {
                        roadSegments[i].lastPoints[0] = roadSegments[i].startPoints[0] + new Vector3(endPoint.x - roadWidth, 0, endPoint.z);
                        roadSegments[i].lastPoints[1] = roadSegments[i].startPoints[1] + new Vector3(endPoint.x + roadWidth, 0, 0);
                        roadSegments[i].lastPoints[1].z = roadSegments[i].lastPoints[0].z;

                        roadSegments[i].interpolatePoints[0] = new Vector3(roadSegments[i].lastPoints[0].x, 0, roadSegments[i].startPoints[0].z);
                        roadSegments[i].interpolatePoints[1] = new Vector3(roadSegments[i].lastPoints[1].x, 0, roadSegments[i].startPoints[1].z);
                        curveDirection = CurveDirection.bottomLeft;
                    }
                    generator.CreateRoadCurve(in roadSegments[i].startPoints, in roadSegments[i].lastPoints, in roadSegments[i].interpolatePoints, 20, curveDirection,i);
                }
            }
            if (i < segmentsCount - 1)
            {
                roadSegments[i+1].startPoints = roadSegments[i].lastPoints;
            }
        }

    }
    private void Initialize()
    {
        roadSegments = new RoadSegment[segmentsCount];
        for (int i = 0; i < segmentsCount; i++)
        {
            roadSegments[i].startPoints = new Vector3[2];
            roadSegments[i].lastPoints = new Vector3[2];
            roadSegments[i].interpolatePoints = new Vector3[2];
        }
        roadSegments[0].startPoints = new Vector3[2] { new Vector3(-roadWidth, 0, 1), new Vector3(roadWidth, 0, 1) };
        roadSegments[0].lastPoints = new Vector3[2] { new Vector3(-roadWidth, 0, lineLenth), new Vector3(roadWidth, 0, lineLenth) };
        roadSegments[1].startPoints = new Vector3[2] { new Vector3(-roadWidth, 0, lineLenth), new Vector3(roadWidth, 0, lineLenth) };
    }

    public int GetSegmentsCount()
    {
        return segmentsCount;
    }
    public struct RoadSegment
    {
        public Vector3[] startPoints, lastPoints;
        public RoadSegmentType type;
        public Vector3[] interpolatePoints;
    }
}

public enum RoadSegmentType
{
    line,
    curve,
}
public enum CurveDirection
{
    topLeft,
    topRight,
    bottomLeft,
    bottomRight
}


