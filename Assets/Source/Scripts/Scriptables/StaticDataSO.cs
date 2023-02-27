using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticDataSO", menuName = "ScriptableObjects/Data/StaticData", order = 1)]
public class StaticDataSO : ScriptableObject
{
    public Vector3 StartPoint;
    public Vector2Int FieldSize;
    public float NodeOffset;
}
