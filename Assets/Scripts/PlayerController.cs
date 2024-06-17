using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 moveDirection = Vector3.forward; 
    [SerializeField] Vector3 definitiveDirection = Vector3.forward;
    [SerializeField] float turnSpeed = 0.5f;
    [SerializeField] float speed;
    [SerializeField] float initialSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float curveSpeed = 0.25f;
    float minXpos, maxXpos;
    float minZpos, maxZpos;
    int turnDir = 1;

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
    [SerializeField] Vector3 tmp;

    float roadWidth;

    private void Start()
    {
        roadWidth = 0.5f;
        maxXpos = RoadPointsGenerator.instatnce.GetRoadWidth() - roadWidth;
        minXpos = -maxXpos;
        rotateObject = transform.GetChild(0);
    }

    private void Update()
    {
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
            if (speed < maxSpeed)
            {
                speed += Time.deltaTime * 0.25f;
            }
            else
            {
                speed = maxSpeed;
            }

            tmp = transform.position;
            if (moveDirection.z != 0)
            {
                Debug.Log("vertiacal");
                definitiveDirection.x = turnSpeed * turnDir * Input.GetAxis("Horizontal") * Time.deltaTime;
                tmp += definitiveDirection * speed * Time.deltaTime;

                if(tmp.x < minXpos)
                {
                    tmp.x = minXpos;
                }
                else if(tmp.x > maxXpos)
                {
                    tmp.x = maxXpos;
                }
            }
            else
            {
                Debug.Log("horizontal");
                definitiveDirection.z = turnSpeed * turnDir * Input.GetAxis("Horizontal") * Time.deltaTime;
                tmp += definitiveDirection * speed * Time.deltaTime;

                if (tmp.z < minZpos)
                {
                    tmp.z = minZpos;
                }
                else if (tmp.z > maxZpos)
                {
                    tmp.z = maxZpos;
                }
            }
            transform.position = tmp;
      
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
            turnDir = 1;
            maxZpos = endVertexes[1].z - roadWidth;
            minZpos = endVertexes[0].z + roadWidth;
        }
        else if(curveDirection == CurveDirection.topRight)
        {
            float offset = startVertexes[1].x - transform.position.x;
            startLerpPoint = transform.position;
            endLerpPoint = new Vector3(endVertexes[1].x, transform.position.y, endVertexes[1].z + offset);
            interpolatePoint = new Vector3(interpolatePoints[1].x - offset, transform.position.y, interpolatePoints[1].z + offset);
            moveDirection = Vector3.right;
            startRotateAngle = 0.01f;
            endRotateAngle = 90;
            endAdditionalAngle = Random.Range(45, 70);
            turnDir = 1;
            maxZpos = endVertexes[0].z - roadWidth;
            minZpos = endVertexes[1].z + roadWidth;
        }
        else if(curveDirection == CurveDirection.bottomLeft)
        {
            float offset = startVertexes[0].z - transform.position.z;
            startLerpPoint = transform.position;
            endLerpPoint = new Vector3(endVertexes[0].x + offset, transform.position.y, endVertexes[1].z);
            interpolatePoint = new Vector3(interpolatePoints[1].x - offset, transform.position.y, interpolatePoints[1].z + offset);
            moveDirection = Vector3.forward;
            startRotateAngle = 90;
            endRotateAngle = 0.01f;
            endAdditionalAngle = Random.Range(-70, -45);
            maxXpos = endVertexes[1].x - roadWidth;
            minXpos = endVertexes[0].x + roadWidth;
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
            maxXpos = endVertexes[1].x - roadWidth;
            minXpos = endVertexes[0].x + roadWidth;
        }
        speed = initialSpeed;
        definitiveDirection = moveDirection;
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


