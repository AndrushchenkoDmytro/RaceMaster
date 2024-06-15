using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] RoadMeshGenarator roadMeshGenarator;
    GameObject tmp = null;
    Vector3 SpawnPos;
    [SerializeField] private MoveDirection startDir = MoveDirection.top;
    private MoveDirection newDir;
    private MoveDirection currentDir;
    private MoveDirection lastDir;
    private int posDirX = 0;
    private int posDirZ = 1;


    private float lineLenth;
    private float curveLenth;
    private int lineCount;
    private int curveCount;

    void Start()
    {
        lineLenth = roadMeshGenarator.GetLineLenth();
        curveLenth = roadMeshGenarator.GetCurveLenth();
        newDir = startDir;
        SpawnPos = new Vector3(0, 0, -lineLenth);

        for (int i = 0; i < 10; i++)
        {
            CreateTrack();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateTrack()
    {
        currentDir = newDir;
        if(currentDir == MoveDirection.top)
        {
            tmp = Instantiate(roadMeshGenarator.GetTopLine());
            posDirZ = 1;
            SpawnPos.z += lineLenth * posDirZ;
            tmp.transform.position = SpawnPos;
            tmp.SetActive(true);
        }
        else if(currentDir == MoveDirection.bottom)
        {
            tmp = Instantiate(roadMeshGenarator.GetBottomLine());
            posDirZ = -1;
            SpawnPos.z += lineLenth * posDirZ;
            tmp.transform.position = SpawnPos;
            tmp.SetActive(true);
        }
        else if(currentDir == MoveDirection.left) 
        {
            tmp = Instantiate(roadMeshGenarator.GetLeftLine());
            posDirX = -1;
            SpawnPos.x += lineLenth * posDirX;
            tmp.transform.position = SpawnPos;
            tmp.SetActive(true);
        }
        else if(currentDir == MoveDirection.right)
        {
            tmp = Instantiate(roadMeshGenarator.GetRightLine());
            posDirX = 1;
            SpawnPos.x += lineLenth * posDirX;
            tmp.transform.position = SpawnPos;
            tmp.SetActive(true);
        }
        else if(currentDir == MoveDirection.topLeft)
        {
            tmp = Instantiate(roadMeshGenarator.GetCurve1());
            if (lastDir == MoveDirection.top)
            {
                posDirX = -1;
                posDirZ = 1;
            }
            else if (lastDir == MoveDirection.right)
            {
                posDirX = 1;
                posDirZ = -1;
            }
            else if (lastDir == MoveDirection.bottomRight)
            {
                if(posDirX == 1 || posDirZ == -1)
                {
                    posDirX = 1;
                    posDirZ = -1;
                }
                else if(posDirX == -1 || posDirZ == 1)
                {
                    posDirX = -1;
                    posDirZ = 1;
                }

            }
            SpawnPos.x += curveLenth * posDirX;
            SpawnPos.z += curveLenth * posDirZ;
            tmp.transform.position = SpawnPos;
            tmp.SetActive(true);
        }
        else if (currentDir == MoveDirection.topRight)
        {
            tmp = Instantiate(roadMeshGenarator.GetCurve2());
            if (lastDir == MoveDirection.top)
            {
                posDirX = 1;
                posDirZ = 1;
            }
            else if (lastDir == MoveDirection.left)
            {
                posDirX = -1;
                posDirZ = -1;
            }
            else if (lastDir == MoveDirection.bottomLeft)
            {
                if (posDirX == -1 || posDirZ == -1)
                {
                    posDirX = -1;
                    posDirZ = -1;
                }
                else if (posDirX == 1 || posDirZ == 1)
                {
                    posDirX = 1;
                    posDirZ = 1;
                }

            }
            SpawnPos.x += curveLenth * posDirX;
            SpawnPos.z += curveLenth * posDirZ;
            tmp.transform.position = SpawnPos;
            tmp.SetActive(true);
        }
        else if (currentDir == MoveDirection.bottomRight)
        {
            tmp = Instantiate(roadMeshGenarator.GetCurve3());
            if (lastDir == MoveDirection.bottom)
            {
                posDirX = 1;
                posDirZ = -1;
            }
            else if (lastDir == MoveDirection.left)
            {
                posDirX = -1;
                posDirZ = 1;
            }
            else if (lastDir == MoveDirection.topLeft)
            {
                if (posDirX == -1 || posDirZ == 1)
                {
                    posDirX = -1;
                    posDirZ = 1;
                }
                else if (posDirX == 1 || posDirZ == 1)
                {
                    posDirX = 1;
                    posDirZ = -1;
                }
            }
            SpawnPos.x += curveLenth * posDirX;
            SpawnPos.z += curveLenth * posDirZ;
            tmp.transform.position = SpawnPos;
            tmp.SetActive(true);
        }
        else if (currentDir == MoveDirection.bottomLeft)
        {
            tmp = Instantiate(roadMeshGenarator.GetCurve3());
            if (lastDir == MoveDirection.bottom)
            {
                posDirX = -1;
                posDirZ = -1;
            }
            else if (lastDir == MoveDirection.right)
            {
                posDirX = 1;
                posDirZ = 1;
            }
            else if (lastDir == MoveDirection.topRight)
            {
                if (posDirX == -1 || posDirZ == -1)
                {
                    posDirX = -1;
                    posDirZ = -1;
                }
                else if (posDirX == 1 || posDirZ == 1)
                {
                    posDirX = 1;
                    posDirZ = 1;
                }
            }
            SpawnPos.x += curveLenth * posDirX;
            SpawnPos.z += curveLenth * posDirZ;
            tmp.transform.position = SpawnPos;
            tmp.SetActive(true);
        }
        ChangeMoveDirection();
    }

    private void ChangeMoveDirection()
    {
        /*if (Mathf.PerlinNoise(Random.Range(1f, 1000f), Random.Range(1f, 1000f)) > 0.6f)
        {
            if (currentDir == MoveDirection.top)
            {
                if (Random.Range(0, 1000) > 500)
                {
                    currentDir = MoveDirection.topLeft;
                }
                else
                {
                    currentDir = MoveDirection.topRight;
                }
                curveCount++;
            }
            else if (currentDir == MoveDirection.bottom)
            {
                if (Random.Range(0, 2000) > 1000)
                {
                    currentDir = MoveDirection.bottomLeft;
                }
                else
                {
                    currentDir = MoveDirection.bottomRight;
                }
                curveCount++;
            }
            else if (currentDir == MoveDirection.left)
            {
                if (Random.Range(0, 1000) > 500)
                {
                    currentDir = MoveDirection.bottomRight;
                }
                else
                {
                    currentDir = MoveDirection.topRight;
                }
                curveCount++;
            }
            else if (currentDir == MoveDirection.right)
            {
                if (Random.Range(0, 2000) > 1000)
                {
                    currentDir = MoveDirection.bottomLeft;
                }
                else
                {
                    currentDir = MoveDirection.topLeft;
                }
                curveCount++;
            }
           
        }*/
        if (lineCount < 5 && curveCount < 2)
        {
            if (currentDir == MoveDirection.top)
            {
                if (Random.Range(0, 1000) > 500)
                {
                    newDir = MoveDirection.topLeft;
                }
                else
                {
                    newDir = MoveDirection.topRight;
                }
                curveCount++;
            }
            else if (currentDir == MoveDirection.bottom)
            {
                if (Random.Range(0, 2000) > 1000)
                {
                    newDir = MoveDirection.bottomLeft;
                }
                else
                {
                    newDir = MoveDirection.bottomRight;
                }
                curveCount++;
            }
            else if (currentDir == MoveDirection.left)
            {
                if (Random.Range(0, 1000) > 500)
                {
                    newDir = MoveDirection.bottomRight;
                }
                else
                {
                    newDir = MoveDirection.topRight;
                }
                curveCount++;
            }
            else if (currentDir == MoveDirection.right)
            {
                if (Random.Range(0, 2000) > 1000)
                {
                    newDir = MoveDirection.bottomLeft;
                }
                else
                {
                    newDir = MoveDirection.topLeft;
                }
                curveCount++;
            }
            else if (currentDir == MoveDirection.topLeft)
            {
                if (lastDir == MoveDirection.top)
                {
                    int x = Random.Range(0, 3000);
                    if (x < 2000)
                    {
                        newDir = MoveDirection.left;
                        lineCount++;
                    }
                    else
                    {
                        newDir = MoveDirection.bottomRight;
                        curveCount++;
                    }
                }
                else if (lastDir == MoveDirection.right)
                {
                    int x = Random.Range(0, 3000);
                    if (x < 2000)
                    {
                        newDir = MoveDirection.bottom;
                        lineCount++;
                    }
                    else
                    {
                        newDir = MoveDirection.bottomRight;
                        curveCount++;
                    }
                }
                else if (lastDir == MoveDirection.bottomRight)
                {
                    if (posDirX == 1 || posDirZ == -1)
                    {
                        newDir = MoveDirection.bottom;
                        lineCount++;
                    }
                    else if (posDirX == -1 || posDirZ == 1)
                    {
                        newDir = MoveDirection.left;
                        lineCount++;
                    }
                }
            }
            else if (currentDir == MoveDirection.bottomRight)
            {
                if (lastDir == MoveDirection.bottom)
                {
                    int x = Random.Range(0, 3000);
                    if (x < 2000)
                    {
                        newDir = MoveDirection.right;
                        lineCount++;
                    }
                    else
                    {
                        newDir = MoveDirection.topLeft;
                        curveCount++;
                    }
                }
                else if (lastDir == MoveDirection.left)
                {
                    int x = Random.Range(0, 3000);
                    if (x < 2000)
                    {
                        newDir = MoveDirection.top;
                        lineCount++;
                    }
                    else
                    {
                        newDir = MoveDirection.topLeft;
                        curveCount++;
                    }
                }
                else if (lastDir == MoveDirection.topLeft)
                {
                    if (posDirX == -1 || posDirZ == 1)
                    {
                        newDir = MoveDirection.top;
                        lineCount++;
                    }
                    else if (posDirX == 1 || posDirZ == 1)
                    {
                        newDir = MoveDirection.right;
                        curveCount++;
                    }
                }
            }
            else if (currentDir == MoveDirection.topRight)
            {
                if (lastDir == MoveDirection.top)
                {
                    int x = Random.Range(0, 3000);
                    if (x < 2000)
                    {
                        newDir = MoveDirection.right;
                        lineCount++;
                    }
                    else
                    {
                        newDir = MoveDirection.bottomLeft;
                        curveCount++;
                    }
                }
                else if (lastDir == MoveDirection.left)
                {
                    int x = Random.Range(0, 3000);
                    if (x < 2000)
                    {
                        newDir = MoveDirection.bottom;
                        lineCount++;
                    }
                    else
                    {
                        newDir = MoveDirection.bottomLeft;
                        curveCount++;
                    }
                }
                else if (lastDir == MoveDirection.bottomLeft)
                {
                    if (posDirX == -1 || posDirZ == -1)
                    {
                        newDir = MoveDirection.bottom;
                        lineCount++;
                    }
                    else if (posDirX == 1 || posDirZ == 1)
                    {
                        newDir = MoveDirection.right;
                        lineCount++;
                    }
                }
            }
            else if (currentDir == MoveDirection.bottomLeft)
            {
                if (lastDir == MoveDirection.bottom)
                {
                    int x = Random.Range(0, 3000);
                    if (x < 2000)
                    {
                        newDir = MoveDirection.left;
                        lineCount++;
                    }
                    else
                    {
                        newDir = MoveDirection.topRight;
                        curveCount++;
                    }
                }
                else if (lastDir == MoveDirection.right)
                {
                    int x = Random.Range(0, 3000);
                    if (x < 2000)
                    {
                        newDir = MoveDirection.top;
                        lineCount++;
                    }
                    else
                    {
                        newDir = MoveDirection.topRight;
                        curveCount++;
                    }
                }
                else if (lastDir == MoveDirection.topRight)
                {
                    if (posDirX == -1 || posDirZ == -1)
                    {
                        newDir = MoveDirection.left;
                        lineCount++;
                    }
                    else if (posDirX == 1 || posDirZ == 1)
                    {
                        newDir = MoveDirection.top;
                        lineCount++;
                    }
                }
            }
        }
        else if (lineCount > 4) //
        {
            if (currentDir == MoveDirection.top)
            {
                newDir = MoveDirection.topRight;
            }
            else if (currentDir == MoveDirection.bottom)
            {
                newDir = MoveDirection.bottomRight;
            }
            else if (currentDir == MoveDirection.left)
            {
                newDir = MoveDirection.topRight;
            }
            else if (currentDir == MoveDirection.right)
            {

                newDir = MoveDirection.topLeft;
            }
            lineCount = 0;
            curveCount++;
        }
        else if (curveCount > 1)
        {
            if (lastDir == MoveDirection.bottomRight)
            {
                if (posDirX == 1 || posDirZ == -1)
                {
                    newDir = MoveDirection.bottom;
                }
                else if (posDirX == -1 || posDirZ == 1)
                {
                    newDir = MoveDirection.left;
                }
            }
            else if (lastDir == MoveDirection.topLeft)
            {
                if (posDirX == -1 || posDirZ == 1)
                {
                    newDir = MoveDirection.top;
                }
                else if (posDirX == 1 || posDirZ == 1)
                {
                    newDir = MoveDirection.right;
                }
            }
            else if (lastDir == MoveDirection.bottomLeft)
            {
                if (posDirX == -1 || posDirZ == -1)
                {
                    newDir = MoveDirection.bottom;
                }
                else if (posDirX == 1 || posDirZ == 1)
                {
                    newDir = MoveDirection.right;
                }
            }
            else
            {
                if (posDirX == -1 || posDirZ == -1)
                {
                    newDir = MoveDirection.left;
                }
                else if (posDirX == 1 || posDirZ == 1)
                {
                    newDir = MoveDirection.top;
                }
            }
            lineCount++;
            curveCount = 0;

        }
        else
        {
            if (currentDir == MoveDirection.top || currentDir == MoveDirection.bottom || currentDir == MoveDirection.right || currentDir == MoveDirection.left)
            {
                lineCount++;
            }
            else
            {
                curveCount++;
            }

        }
        lastDir = currentDir;
    }
}

public enum MoveDirection
{
    top,
    bottom,
    left,
    right,
    topLeft,
    topRight,
    bottomLeft,
    bottomRight
}
