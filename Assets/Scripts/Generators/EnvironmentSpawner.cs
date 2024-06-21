using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static RoadPointsGenerator;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField] EnvironmentScriptableObject[] environment;

    Vector3 spawnPoint = Vector3.zero;
    GameObject spawnObject;

    private void Awake()
    {

    }

    public void SpawnEnvironment(RoadSegment[] roadSegments, float roadLenth, float roadWidth)
    {
        for (int i = 0; i < roadSegments.Length; i++)
        {
            if (roadSegments[i].type == RoadSegmentType.line)
            {


                if (roadSegments[i].startPoints[0].z == roadSegments[i].startPoints[1].z) // vertical line
                {
                    spawnObject = SelectRandomEnvironmentGameObject(out int indexL);
                    spawnPoint = new Vector3(roadSegments[i].startPoints[0].x - environment[indexL].size - 0.1f, 0, roadSegments[i].startPoints[0].z + roadLenth * 0.5f);
                    Instantiate(spawnObject, spawnPoint, spawnObject.transform.rotation);

                    spawnObject = SelectRandomEnvironmentGameObject(out int indexR);
                    spawnPoint = new Vector3(roadSegments[i].startPoints[1].x + environment[indexR].size + 0.1f, 0, roadSegments[i].startPoints[1].z + roadLenth * 0.5f);
                    Instantiate(spawnObject, spawnPoint, new Quaternion(0, 180, 0, 0));
                }
                else // horizontal line
                {
                    if(roadSegments[i].startPoints[0].z >= roadSegments[i].startPoints[1].z) // horizontal right
                    {
                        spawnObject = SelectRandomEnvironmentGameObject(out int indexU);
                        spawnPoint = new Vector3(roadSegments[i].startPoints[0].x + roadLenth * 0.5f, 0, roadSegments[i].startPoints[0].z + environment[indexU].size + 0.1f);
                        Instantiate(spawnObject, spawnPoint, new Quaternion(0, 90, 0, 0));

                        spawnObject = SelectRandomEnvironmentGameObject(out int indexB);
                        spawnPoint = new Vector3(roadSegments[i].startPoints[1].x + roadLenth * 0.5f, 0, roadSegments[i].startPoints[1].z - environment[indexB].size - 0.1f);
                        Instantiate(spawnObject, spawnPoint, new Quaternion(0, 270, 0, 0));
                    }
                    else
                    {
                        spawnObject = SelectRandomEnvironmentGameObject(out int indexU);
                        spawnPoint = new Vector3(roadSegments[i].startPoints[1].x - roadLenth * 0.5f, 0, roadSegments[i].startPoints[0].z - environment[indexU].size - 0.1f);
                        Instantiate(spawnObject, spawnPoint, new Quaternion(0, 90, 0, 0));

                        spawnObject = SelectRandomEnvironmentGameObject(out int indexB);
                        spawnPoint = new Vector3(roadSegments[i].startPoints[0].x - roadLenth * 0.5f, 0, roadSegments[i].startPoints[1].z + environment[indexB].size + 0.1f);
                        Instantiate(spawnObject, spawnPoint, new Quaternion(0, 270, 0, 0));
                    }
                }
            }
        }
    }

    private GameObject SelectRandomEnvironmentGameObject(out int index)
    {
        index = Random.Range(0, environment.Length);
        return environment[index].environment;
    }
}
