using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CurveTrigger : MonoBehaviour
{
    Vector3[] startVertexes;
    Vector3[] endVertexes;
    Vector3[] interpolatePoints;
    CurveDirection curveDirection;
    int index;
    public void SetCurveValues(Vector3[] startVertexes, Vector3[] endVertexes, Vector3[] interpolatePoints, CurveDirection curveDirection, int index)
    {
        this.startVertexes = startVertexes;
        this.endVertexes = endVertexes;
        this.interpolatePoints = interpolatePoints;
        this.curveDirection = curveDirection;
        this.index = index;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            pc.ChangeMovementToCurve(startVertexes, endVertexes, interpolatePoints, curveDirection);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().SetCameraIncreaseOffsetValues(curveDirection);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            if (RoadPointsGenerator.instatnce.roadSegments[index+1].type == RoadSegmentType.line)
            {
                pc.ChangeMovementToLine();
            }
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().SetCameraDecreaseOffsetValues(curveDirection);
        }
    }
}
