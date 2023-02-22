using UnityEngine;

public class StaticData
{
    internal Vector3 StartPoint;
    internal Vector2Int FieldSize;
    internal float NodeOffset;

    public StaticData(StaticDataSO staticDataSO)
    {
        StartPoint = staticDataSO.StartPoint;
        FieldSize = staticDataSO.FieldSize;
        NodeOffset = staticDataSO.NodeOffset;
    }
}
