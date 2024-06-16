using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoadPointsGenerator;

public class PlayerController : MonoBehaviour
{
    Vector3 moveDirection = Vector3.forward; 
    [SerializeField] float speed;
    [SerializeField] float initialSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float curveSpeed = 0.25f;
    [SerializeField] RoadPointsGenerator rpg;
    [SerializeField] CinemachineVirtualCamera cam;

    bool curveMovement = false;
    [SerializeField] float curveTime = 0;
    Vector3 startLerpPoint = Vector3.zero;
    Vector3 endLerpPoint = Vector3.zero;
    Vector3 interpolatePoint = Vector3.zero;
    float startRotateAngle = 0;
    float endRotateAngle;

    Transform rotateObject;
    [SerializeField] float startAdditionalAngle = 30;
    [SerializeField] float endAdditionalAngle;

    Vector3 lastPos = Vector3.zero;
    [SerializeField] float deltaPos;

    [SerializeField] CinemachineTransposer transposer;


    private void Start()
    {
        var transposer = cam.GetCinemachineComponent<CinemachineTransposer>();

        rotateObject = transform.GetChild(0);
    }

    private void Update()
    {
        deltaPos = Vector3.Distance(transform.position,lastPos);
        lastPos = transform.position;
        if (curveMovement)
        {
            if(curveTime < 1f)
            {
                transform.eulerAngles = new Vector3(0, Mathf.Lerp(startRotateAngle, endRotateAngle, curveTime), 0);

                if (curveTime < 0.5f)
                {
                    rotateObject.localEulerAngles = new Vector3(0, Mathf.Lerp(startAdditionalAngle, endAdditionalAngle, curveTime), 0);
                }
                else
                {
                    rotateObject.localEulerAngles = new Vector3(0, Mathf.Lerp(endAdditionalAngle, startAdditionalAngle,curveTime),0);   
                }

                curveTime += Time.deltaTime * curveSpeed;
            }
        }
        else
        {
            if(speed < maxSpeed)
            {
                speed += Time.deltaTime * 0.25f; 
            }
            else
            {
                speed = maxSpeed;
            }
            //moveDirection.x = curveSpeed * Input.GetAxis("Horizontal");
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
        transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        curveMovement = true;
        curveTime = 0;
        startAdditionalAngle = 0;


        if (curveDirection == CurveDirection.topLeft)
        {
            float offset = startVertexes[1].x - transform.position.x;
            startLerpPoint = transform.position;
            endLerpPoint = new Vector3(endVertexes[1].x, transform.position.y, endVertexes[1].z - offset);
            interpolatePoint = new Vector3(interpolatePoints[1].x - offset, transform.position.y, interpolatePoints[1].z - offset);
            moveDirection = Vector3.left;
            startRotateAngle = 0.01f;
            endRotateAngle = -90;
            endAdditionalAngle = Random.Range(-70,-45);
        }
        else if(curveDirection == CurveDirection.topRight)
        {
            float offset = startVertexes[1].x - transform.position.x;
            startLerpPoint = transform.position;
            endLerpPoint = new Vector3(endVertexes[1].x, transform.position.y, endVertexes[0].z - offset);
            interpolatePoint = new Vector3(interpolatePoints[1].x - offset, transform.position.y, interpolatePoints[1].z + offset);
            moveDirection = Vector3.right;
            startRotateAngle = 0.01f;
            endRotateAngle = 90;
            endAdditionalAngle = Random.Range(45, 70);
        }
        else if(curveDirection == CurveDirection.bottomLeft)
        {
            float offset = startVertexes[0].z - transform.position.z;
            startLerpPoint = transform.position;
            endLerpPoint = new Vector3(endVertexes[1].x - offset, transform.position.y, endVertexes[1].z);
            interpolatePoint = new Vector3(interpolatePoints[1].x - offset, transform.position.y, interpolatePoints[1].z + offset);
            moveDirection = Vector3.forward;
            startRotateAngle = 90;
            endRotateAngle = 0.01f;
            endAdditionalAngle = Random.Range(-70, -45);
        }
        else if(curveDirection == CurveDirection.bottomRight) 
        {
            float offset = startVertexes[1].z - transform.position.z;
            startLerpPoint = transform.position;
            endLerpPoint = new Vector3(endVertexes[1].x - offset, transform.position.y, endVertexes[1].z);
            interpolatePoint = new Vector3(interpolatePoints[0].x + offset, transform.position.y, interpolatePoints[0].z + offset);
            moveDirection = Vector3.forward;
            startRotateAngle = -90;
            endRotateAngle = 0.01f;
            endAdditionalAngle = Random.Range(45, 70);
        }
        speed = initialSpeed;
    }
    
    public void ChangeMovementToLine()
    {
        //if (curveDirection == CurveDirection.topLeft)
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(startLerpPoint, 0.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(endLerpPoint, 0.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(interpolatePoint, 0.5f);
    }
}


