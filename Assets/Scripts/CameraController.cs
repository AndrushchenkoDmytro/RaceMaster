using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool changeCameraOffset;
    [SerializeField] private bool zoomOut;
    private float followCameraTime;
    [SerializeField] private float cameraFollowInitialSpeed;
    private float cameraFollowEndSpeed;
    private Vector3 followCameraOffset;
    [SerializeField] Vector3 followCameraMinOffset = new Vector3(0, 2f, -4);
    [SerializeField] Vector3 followCameraMaxOffset = new Vector3(0, 3f, -2f);

    [SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] CinemachineTransposer transposer;

    void Start()
    {
        followCameraOffset = followCameraMinOffset;
    }

    void LateUpdate()
    {
        if (changeCameraOffset)
        {
            if (zoomOut)
            {
                followCameraTime += cameraFollowEndSpeed * Time.deltaTime;
                followCameraOffset = Vector3.Lerp(followCameraOffset, followCameraMaxOffset, cameraFollowEndSpeed * Time.deltaTime);
                float distance = Vector3.Distance(followCameraOffset, followCameraMaxOffset);
                if (distance <= 0.05f)
                {
                    zoomOut = false;
                    followCameraTime = 0;
                    followCameraMinOffset.x = 0f;
                    cameraFollowEndSpeed = cameraFollowInitialSpeed;
                }
                else
                {
                    cameraFollowEndSpeed += Time.deltaTime * 0.75f;
                }

            }
            else
            {
                followCameraTime += cameraFollowEndSpeed * Time.deltaTime;
                followCameraOffset = Vector3.Lerp(followCameraOffset, followCameraMinOffset, cameraFollowEndSpeed * Time.deltaTime);
                float distance = Vector3.Distance(followCameraOffset, followCameraMinOffset);
                if (distance <= 0.05f)
                {
                    changeCameraOffset = false;
                }
                else
                {
                    cameraFollowEndSpeed += Time.deltaTime * 0.75f;
                }
            }
            transposer.m_FollowOffset = followCameraOffset;
        }
    }

    public void SetCameraIncreaseOffsetValues(CurveDirection curveDirection)
    {
        transposer = vCam.GetCinemachineComponent<CinemachineTransposer>();
        changeCameraOffset = true;
        zoomOut = true;
        followCameraTime = 0;
        cameraFollowEndSpeed = cameraFollowInitialSpeed;

        if (curveDirection == CurveDirection.topLeft)
        {
            followCameraMaxOffset.x = -1f;
        }
        else if (curveDirection == CurveDirection.topRight)
        {
            followCameraMaxOffset.x = 1f;
        }
        else if (curveDirection == CurveDirection.bottomLeft)
        {
            followCameraMaxOffset.x = -1f;
        }
        else if (curveDirection == CurveDirection.bottomRight)
        {
            followCameraMaxOffset.x = 1f;
        }
    }

    public void SetCameraDecreaseOffsetValues(CurveDirection curveDirection)
    {
        changeCameraOffset = true;
        zoomOut = false;
        followCameraTime = 0;
        followCameraMinOffset.x = 0f;
        cameraFollowEndSpeed = cameraFollowInitialSpeed;

    }
}
