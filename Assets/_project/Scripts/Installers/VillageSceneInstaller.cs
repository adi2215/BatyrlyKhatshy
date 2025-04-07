using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class VillageSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {

        PlayerBindings();
        CameraBindings();

    }
    private void PlayerBindings()
    {
        Container.Bind<GameInputAction>().FromInstance(new GameInputAction()).AsCached();
        Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
        Container.Bind<CharacterController>().FromComponentInHierarchy().AsCached().NonLazy();
    }
    private void CameraBindings()
    {
        Container.Bind<CinemachineVirtualCamera>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerCamera>().AsSingle().WithArguments(true);
    }

}