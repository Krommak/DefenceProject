using System;
using UnityEngine;

[Serializable]
public struct WeightComponent
{
    public Transform Transform;
    public Vector2Int IncreaseWeightAndOffset;
    public Vector2Int DecreaseWeightAndOffset;
}
