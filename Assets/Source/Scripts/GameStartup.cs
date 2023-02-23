using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AddressableAssets.Initialization;
using Voody.UniLeo;

public class GameStartup : MonoBehaviour
{
    [SerializeField]
    StaticDataSO _staticDataSO;
    EcsWorld _world;
    EcsSystems _initSystems;
    EcsSystems _updateSystems;
    EcsSystems _fixedUpdateSystems;
    StaticData _staticData;
    RuntimeData _runtimeData;

    void Start()
    {
        _world = new EcsWorld();
        InitData();
        CreateInitSystems();
        CreateUpdateSystems();
        CreateFixedUpdateSystems();
        Inject();
        _initSystems.Init();
        _updateSystems.Init();
        _fixedUpdateSystems.Init();
    }

    void InitData()
    {
        _staticData = new StaticData(_staticDataSO);
        _runtimeData = new RuntimeData();
    }

    void CreateInitSystems()
    {
        _initSystems = new EcsSystems(_world)
            .Add(new FieldInitSystem())
            .Add(new CalculateWeight());
        _initSystems.ConvertScene();
    }

    void CreateUpdateSystems()
    {
        _updateSystems = new EcsSystems(_world);
        _updateSystems.ConvertScene();
    }

    void CreateFixedUpdateSystems()
    {
        _fixedUpdateSystems = new EcsSystems(_world);
        _fixedUpdateSystems.ConvertScene();
    }

    void Inject()
    {
        _initSystems.Inject(_staticData)
            .Inject(_runtimeData);
        _updateSystems.Inject(_staticData)
            .Inject(_runtimeData);
        _fixedUpdateSystems.Inject(_staticData)
            .Inject(_runtimeData);
    }

    void Update()
    {
        _updateSystems.Run();
    }

    void FixedUpdate()
    {

        _fixedUpdateSystems.Run();
    }
}
