using Leopotam.Ecs;
using UnityEngine;

public class FieldInitSystem : IEcsInitSystem
{
    StaticData _staticData;
    RuntimeData _runtimeData;
    
    public void Init()
    {
        _runtimeData.StandartField = new Node[_staticData.FieldSize.x, _staticData.FieldSize.y];

        var offset = _staticData.NodeOffset;
        var PosX = _staticData.StartPoint.x + offset;
        var PosY = _staticData.StartPoint.y;
        var PosZ = _staticData.StartPoint.z + offset;

        for (int z = 0; z < _staticData.FieldSize.y; z++)
        {
            for (int x = 0; x < _staticData.FieldSize.x; x++)
            {
                ref var node = ref _runtimeData.StandartField[x, z];

                node.Position = new Vector3(PosX, PosY, PosZ);

                PosX += offset * 2;
            }
            PosX = _staticData.StartPoint.x + offset;
            PosZ += offset * 2;
        }
    }
}
