using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class RunnerSceneInstaller : MonoInstaller
{
    [SerializeField] private bool _isFollowCamera = true;

    [Header("Edge Positions")]
    [SerializeField]
    Transform _spawnLocation;

    [SerializeField]
    Transform _killLocation;

    [Header("Side Positions")]
    [SerializeField]
    Transform _farLocation;

    [SerializeField]
    Transform _nearLocation;


    public override void InstallBindings()
    {

        PlayerBindings();
        CameraBindings();
        GenerationBinding();
        EnemyBindings();
        ManagerBindings();

    }


    private void PlayerBindings()
    {
        Container.Bind<GameInputAction>().FromInstance(new GameInputAction()).AsCached();
        Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerMouseInput>().AsSingle();
        Container.Bind<CharacterController>().FromComponentInHierarchy().AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<BoneInput>().AsSingle();
        Container.Bind<Dog>().FromComponentInHierarchy().AsSingle(); 
        Container.Bind<Bone>().FromComponentInHierarchy().AsSingle();
    }
    private void CameraBindings()
    {
        Container.Bind<CinemachineVirtualCamera>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerCamera>().AsSingle().WithArguments(_isFollowCamera);
    }
    private void GenerationBinding()
    {
        Container.Bind<Transform>().WithId("SpawnLocation").FromInstance(_spawnLocation).AsTransient();
        Container.Bind<Transform>().WithId("KillLocation").FromInstance(_killLocation).AsTransient();

        Container.Bind<Transform>().WithId("FarLocation").FromInstance(_farLocation).AsTransient();
        Container.Bind<Transform>().WithId("NearLocation").FromInstance(_nearLocation).AsTransient();

        Container.Bind<ObstacleGenerator>().FromComponentInHierarchy().AsSingle();

    }
    private void EnemyBindings()
    {
        Container.Bind<NotifyManager>().FromComponentInHierarchy().AsSingle();    
        Container.Bind<FollowEnemyCreator>().FromComponentInHierarchy().AsSingle();            
        Container.Bind<RangeEnemyCreator>().FromComponentInHierarchy().AsSingle();            
    }
    private void ManagerBindings()
    {
        Container.Bind<RunnerGameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<EnemyManager>().AsTransient();

    }
}   