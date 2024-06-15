using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoadPointsGenerator;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 moveDirection = Vector3.forward;
    [SerializeField] float speed;
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
    bool stopAddAngele = true;
    [SerializeField] float startAdditionalAngle = 30;
    [SerializeField] float endAdditionalAngle;

    [SerializeField] bool changeCameraOffset = false;
    [SerializeField] bool zoomOut = false;
    Vector3 followCameraOffset = new Vector3(0,1,-4);
    [SerializeField] Vector3 followCameraMinOffset = new Vector3(0, 1, -4);
    [SerializeField] Vector3 followCameraMaxOffset = new Vector3(0,3f,-2f);
    [SerializeField] float cameraFollowSpeed = 0.150f;
    float followCameraTime = 0;

    [SerializeField] CinemachineTransposer transposer;


    private void Start()
    {
        var transposer = cam.GetCinemachineComponent<CinemachineTransposer>();

        rotateObject = transform.GetChild(0);
    }

    private void Update()
    {
        if (curveMovement)
        {
            if(curveTime < 1f)
            {
                transform.position = RoadMeshGenarator.QuadroLerp(startLerpPoint, interpolatePoint, endLerpPoint, curveTime);
                transform.eulerAngles = new Vector3(0, Mathf.Lerp(startRotateAngle, endRotateAngle, curveTime), 0);
                curveTime += Time.deltaTime * curveSpeed;

                if (curveTime < 0.5f)
                {
                    rotateObject.localEulerAngles = new Vector3(0, Mathf.Lerp(startAdditionalAngle, endAdditionalAngle, curveTime), 0);
                }
                else
                {
                    rotateObject.localEulerAngles = new Vector3(0, Mathf.Lerp(endAdditionalAngle, startAdditionalAngle,curveTime),0);   
                }
            }
            else
            {
                curveTime = 0;
                curveMovement = false;
                transform.position = endLerpPoint;
                transform.eulerAngles = new Vector3(0, endRotateAngle, 0);
            }
        }
        else
        {
            //moveDirection.x = curveSpeed * Input.GetAxis("Horizontal");
            transform.position += moveDirection * speed * Time.deltaTime;
        }

        if (changeCameraOffset)
        {
            if (zoomOut)
            {
                followCameraTime += cameraFollowSpeed * Time.deltaTime;
                followCameraOffset = new Vector3(Mathf.Lerp(followCameraMinOffset.x, followCameraMaxOffset.x, followCameraTime ), Mathf.Lerp(followCameraMinOffset.y, followCameraMaxOffset.y, followCameraTime), Mathf.Lerp(followCameraMinOffset.z, followCameraMaxOffset.z, followCameraTime));
                if(followCameraOffset.y >= followCameraMaxOffset.y)
                {
                    changeCameraOffset = false;
                }
                
            }
            else
            {
                followCameraTime += cameraFollowSpeed * Time.deltaTime;
                followCameraOffset = new Vector3(Mathf.Lerp(followCameraMaxOffset.x, followCameraMinOffset.x, followCameraTime), Mathf.Lerp(followCameraMaxOffset.y, followCameraMinOffset.y, followCameraTime), Mathf.Lerp(followCameraMaxOffset.z, followCameraMinOffset.z, followCameraTime));
                if (followCameraOffset.y <= followCameraMinOffset.y)
                {
                    changeCameraOffset = false;
                }
                
            }
            transposer.m_FollowOffset = followCameraOffset;
        }
    }   


    public void ChangeMovementToCurve(in Vector3[] startVertexes, in Vector3[] endVertexes, in Vector3[] interpolatePoints, CurveDirection curveDirection)
    {
        transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        curveMovement = true;
        curveTime = 0;
        followCameraTime = 0;

        startAdditionalAngle = 0;
        followCameraOffset = transposer.m_FollowOffset;

        changeCameraOffset = true;
        zoomOut = true;
        Debug.Log("ZoomOut = " + zoomOut);

        if (curveDirection == CurveDirection.topLeft)
        {
            float offset = startVertexes[1].x - transform.position.x;
            startLerpPoint = transform.position;
            endLerpPoint = new Vector3(endVertexes[1].x, transform.position.y, endVertexes[1].z - offset);
            interpolatePoint = new Vector3(interpolatePoints[1].x - offset, transform.position.y, interpolatePoints[1].z - offset);
            moveDirection = Vector3.left;
            startRotateAngle = 0.01f;
            endRotateAngle = -95;
            endAdditionalAngle = Random.Range(-50,-25);
            followCameraMaxOffset.x = -3f;

        }
        else if(curveDirection == CurveDirection.topRight)
        {
            float offset = startVertexes[1].x - transform.position.x;
            startLerpPoint = transform.position;
            endLerpPoint = new Vector3(endVertexes[1].x, transform.position.y, endVertexes[0].z - offset);
            interpolatePoint = new Vector3(interpolatePoints[1].x - offset, transform.position.y, interpolatePoints[1].z + offset);
            moveDirection = Vector3.right;
            startRotateAngle = 0.01f;
            endRotateAngle = 95;
            endAdditionalAngle = Random.Range(25, 50);
            followCameraMaxOffset.x = 3f;
        }
        else if(curveDirection == CurveDirection.bottomLeft)
        {
            float offset = startVertexes[0].z - transform.position.z;
            startLerpPoint = transform.position;
            endLerpPoint = new Vector3(endVertexes[1].x - offset, transform.position.y, endVertexes[1].z);
            interpolatePoint = new Vector3(interpolatePoints[1].x - offset, transform.position.y, interpolatePoints[1].z + offset);
            moveDirection = Vector3.forward;
            startRotateAngle = 95;
            endRotateAngle = 0.01f;
            endAdditionalAngle = Random.Range(-50, -25);
            followCameraMaxOffset.x = -3f;
        }
        else if(curveDirection == CurveDirection.bottomRight) 
        {
            float offset = startVertexes[1].z - transform.position.z;
            startLerpPoint = transform.position;
            endLerpPoint = new Vector3(endVertexes[1].x - offset, transform.position.y, endVertexes[1].z);
            interpolatePoint = new Vector3(interpolatePoints[0].x + offset, transform.position.y, interpolatePoints[0].z + offset);
            moveDirection = Vector3.forward;
            startRotateAngle = -95;
            endRotateAngle = 0.01f;
            endAdditionalAngle = Random.Range(25, 50);
            followCameraMaxOffset.x = 3f;
        }
    }
    
    public void ChangeMovementToLine()
    {
        followCameraTime = 0;
        zoomOut = false;
        changeCameraOffset = true;
        Debug.Log("ZoomOut = " + zoomOut);

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

    /*
    public enum Axel 
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider; 
        public Axel axel;
    }

    public float maxAcceleration = 30.0f; 
    public float brakeAcceleration = 50.0f;

    public float turnSensitivity = 1.0f; 
    public float maxSteerAngle = 30.0f;

    public List<Wheel> wheels;
    float moveInput;
    float steerInput;
    private Rigidbody carRb;
    [SerializeField] Vector3 centerOfMass;
    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = centerOfMass;
    }

    void Update()
    {
        GetInputs();
    }
    void LateUpdate()
    {
        Move();
        Steer();
    }
    void GetInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
        }
    }

    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }*/
}


