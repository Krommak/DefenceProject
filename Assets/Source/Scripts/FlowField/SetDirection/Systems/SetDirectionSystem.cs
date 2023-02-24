using Leopotam.Ecs;
using UnityEngine;

public class SetDirectionSystem : IEcsInitSystem
{
    StaticData _staticData;
    RuntimeData _runtimeData;
    EcsFilter<PlayerComponent> _players;
    Node[,] nodes;
    EcsFilter<DebugDrawerComponent> _debug = null;
    public void Init()
    {
        foreach (var player in _players)
        {
            var num = _players.Get1(player).PlayerNum;

            nodes = _runtimeData.PlayField.GetFieldsForPlayer(num);

            for (int x = 0; x <= _runtimeData.PlayField.GetFieldSize.x; x++)
            {
                for (int z = 0; z <= _runtimeData.PlayField.GetFieldSize.y; z++)
                {
                    SetDirection(new Vector2Int(x, z));
                }
            }
        }
        foreach (var deb in _debug)
        {
            ref var debug = ref _debug.Get1(deb);

            debug.Drawer.SetNodes(_runtimeData.PlayField.GetFieldsForPlayer(1));
            debug.Drawer.DrawDir = true;
        }
    }

    void SetDirection(Vector2Int center)
    {
        ref Node node = ref nodes[center.x, center.y];
        var actualWeight = node.Weight;
        if (actualWeight <= 0) return;

        var firstX = Mathf.Clamp(center.x - 1, 0, _runtimeData.PlayField.GetFieldSize.x);
        var firstZ = Mathf.Clamp(center.y - 1, 0, _runtimeData.PlayField.GetFieldSize.y);
        var finalX = Mathf.Clamp(center.x + 1, 0, _runtimeData.PlayField.GetFieldSize.x);
        var finalZ = Mathf.Clamp(center.y + 1, 0, _runtimeData.PlayField.GetFieldSize.y);

        for (int x = firstX; x <= finalX; x++)
        {
            for (int z = firstZ; z <= finalZ; z++)
            {
                if(nodes[x, z].Weight > actualWeight)
                {
                    actualWeight = nodes[x, z].Weight;
                    node.Direction = (nodes[x, z].Position - node.Position).normalized;
                }
            }
        }
    }
}
