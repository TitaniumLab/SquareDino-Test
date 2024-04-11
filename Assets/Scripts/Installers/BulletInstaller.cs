using UnityEngine;
using Zenject;

public class BulletInstaller : MonoInstaller
{
    [SerializeField] private BulletSpawner _bulletSpawner;

    public override void InstallBindings() =>
        Container.Bind<IShooting>().FromInstance(_bulletSpawner);

}