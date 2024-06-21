using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnvironmentScriptableObject", menuName = "ScriptableObjects/Environment")]
public class EnvironmentScriptableObject : ScriptableObject
{
    public GameObject environment;
    public float size;
}
