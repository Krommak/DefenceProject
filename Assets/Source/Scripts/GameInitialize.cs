using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitialize : MonoBehaviour
{
    EcsWorld _world;
    EcsSystems _updateSystems;
    EcsSystems _fixedUpdateSystems;
    StaticData _staticData;
    RuntimeData _runtimeData;
    void Start()
    {
        _world = new EcsWorld();

        _updateSystems.Init();
    }

    void CreateUpdateSystems()
    {
        _updateSystems = new EcsSystems(_world)
            .Add(new FieldInitSystem());

    }
    void CreateFixedUpdateSystems()
    {
        _fixedUpdateSystems = new EcsSystems(_world);
    }

    void Inject()
    {
        //_updateSystems.Inject();
        //_fixedUpdateSystems.Inject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
