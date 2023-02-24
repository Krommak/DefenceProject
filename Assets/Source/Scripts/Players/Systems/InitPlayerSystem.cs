using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

public class InitPlayerSystem : IEcsInitSystem
{
    EcsFilter<PlayerComponent> players = null;
    public void Init()
    {
        int count = 0;
        foreach (var player in players)
        {
            ref var component = ref players.Get1(player);
            component.PlayerNum = count;
            count++;
        }
    }
}
