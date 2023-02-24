using Leopotam.Ecs;
using System;
using UnityEngine;

public class CalculateWeight : IEcsInitSystem
{
    EcsFilter<DebugDrawerComponent> _debug = null;
    StaticData _staticData;
    RuntimeData _runtimeData;
    EcsFilter<WeightComponent, PlayerComponent> _weights = null;
    int actualPlayer = 0;

    public void Init()
    {
        foreach (var item in _weights)
        {
            ref var weight = ref _weights.Get1(item);
            actualPlayer = _weights.Get2(item).PlayerNum;
            Calculate(weight);
            foreach (var deb in _debug)
            {
                ref var debug = ref _debug.Get1(deb);

                debug.Drawer.SetNodes(_runtimeData.PlayField.GetFieldsForPlayer(0));
            }
        }
    }

    void Calculate(WeightComponent weightComponent)
    {
        var center = GetCenter(weightComponent.Transform.position);
        SetWeight(center, weightComponent.IncreaseWeightAndOffset);
        SetWeight(center, weightComponent.DecreaseWeightAndOffset);
    }

    void SetWeight(Vector2Int center, Vector2Int weightAndOffset)
    {
        var xSize = _runtimeData.PlayField.GetFieldSize.x;
        var zSize = _runtimeData.PlayField.GetFieldSize.y;
        var xStart = Mathf.Clamp(center.x - weightAndOffset.y, 0, xSize);
        var zStart = Mathf.Clamp(center.y - weightAndOffset.y, 0, zSize);
        var xFinish = Mathf.Clamp(center.x + weightAndOffset.y, 0, xSize);
        var zFinish = Mathf.Clamp(center.y + weightAndOffset.y, 0, zSize);
        var nodes = _runtimeData.PlayField.GetFieldsForPlayer(actualPlayer);
        for (int x = xStart; x <= xFinish; x++)
        {
            for (int z = zStart; z <= zFinish; z++)
            {
                ref var node = ref nodes[x, z];
                var xMax = x >= center.x ? x : center.x;
                var xMin = x < center.x ? x : center.x;
                var zMax = z >= center.y ? z : center.y;
                var zMin = z < center.y ? z : center.y;
                var remoteness = Math.Abs(xMax - xMin + zMax - zMin);
                float influence = weightAndOffset.x / (remoteness == 0 ? 1 : remoteness);
                node.Weight += influence;
            }
        }
    }

    Vector2Int GetCenter(Vector3 pos)
    {
        var xSize = _runtimeData.PlayField.GetFieldSize.x;
        var zSize = _runtimeData.PlayField.GetFieldSize.y;
        var centerPosition = new Vector2(
            (float)Math.Round(pos.x, 1, MidpointRounding.ToEven),
            (float)Math.Round(pos.z, 1, MidpointRounding.ToEven));

        return new Vector2Int(
            Mathf.Clamp((int)(centerPosition.x / (_staticData.NodeOffset * 2)), 0, xSize),
            Mathf.Clamp((int)(centerPosition.y / (_staticData.NodeOffset * 2)), 0, zSize)
            );
    }
}
