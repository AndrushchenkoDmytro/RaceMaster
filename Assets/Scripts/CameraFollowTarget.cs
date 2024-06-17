using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraFollowTarget : MonoBehaviour
{
    Vector3 moveDirection = Vector3.forward;
    [SerializeField] float speed;
    [SerializeField] float initialSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float curveSpeed = 0.25f;

    bool curveMovement = false;
    [SerializeField] float curveTime = 0;
    Vector3 startLerpPoint = Vector3.zero;
    Vector3 endLerpPoint = Vector3.zero;
    Vector3 interpolatePoint = Vector3.zero;
    float startRotateAngle = 0;
    float endRotateAngle;


    private void Update()
    {
        if (curveMovement)
        {
            if (curveTime < 1f)
            {
                transform.eulerAngles = new Vector3(0, Mathf.Lerp(startRotateAngle, endRotateAngle, curveTime), 0);
                curveTime += Time.deltaTime * curveSpeed;
            }
        }
        else
        {
            if (speed < maxSpeed)
            {
                speed += Time.deltaTime * 0.25f;
            }
            else
            {
                speed = maxSpeed;
            }
            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (curveMovement)
        {
            if (curveTime < 1f)
            {
                transform.position = RoadMeshGenarator.QuadroLerp(startLerpPoint, interpolatePoint, endLerpPoint, curveTime);
            }
            else
            {
                curveTime = 0;
                curveMovement = false;
                transform.position = endLerpPoint;
                transform.eulerAngles = new Vector3(0, endRotateAngle, 0);
            }
        }

    }


    public void ChangeMovementToCurve(in Vector3[] startVertexes, in Vector3[] endVertexes, in Vector3[] interpolatePoints, CurveDirection curveDirection)
    {
        curveMovement = true;
        curveTime = 0;

        if (curveDirection == CurveDirection.topLeft)
        {
            float offset = startVertexes[1].x - transform.position.x;
            startLerpPoint = transform.position;
            endLerpPoint = new Vector3(endVertexes[1].x, transform.position.y, endVertexes[1].z - offset);
            interpolatePoint = new Vector3(interpolatePoints[1].x - offset, transform.position.y, interpolatePoints[1].z - offset);
            moveDirection = Vector3.left;
            startRotateAngle = 0.01f;
            endRotateAngle = -90;
        }
        else if (curveDirection == CurveDirection.topRight)
        {
            float offset = startVertexes[1].x - transform.position.x;
            startLerpPoint = transform.position;
            endLerpPoint = new Vector3(endVertexes[1].x, transform.position.y, endVertexes[0].z - offset);
            interpolatePoint = new Vector3(interpolatePoints[1].x - offset, transform.position.y, interpolatePoints[1].z + offset);
            moveDirection = Vector3.right;
            startRotateAngle = 0.01f;
            endRotateAngle = 90;
        }
        else if (curveDirection == CurveDirection.bottomLeft)
        {
            float offset = startVertexes[0].z - transform.position.z;
            startLerpPoint = transform.position;
            endLerpPoint = new Vector3(endVertexes[1].x - offset, transform.position.y, endVertexes[1].z);
            interpolatePoint = new Vector3(interpolatePoints[1].x - offset, transform.position.y, interpolatePoints[1].z + offset);
            moveDirection = Vector3.forward;
            startRotateAngle = 90;
            endRotateAngle = 0.01f;
        }
        else if (curveDirection == CurveDirection.bottomRight)
        {
            float offset = startVertexes[1].z - transform.position.z;
            startLerpPoint = transform.position;
            endLerpPoint = new Vector3(endVertexes[1].x - offset, transform.position.y, endVertexes[1].z);
            interpolatePoint = new Vector3(interpolatePoints[0].x + offset, transform.position.y, interpolatePoints[0].z + offset);
            moveDirection = Vector3.forward;
            startRotateAngle = -90;
            endRotateAngle = 0.01f;
        }
        speed = initialSpeed;
    }
}
