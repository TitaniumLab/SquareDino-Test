using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;
using Zenject;

public class BulletSpawner : MonoBehaviour, IShooting
{
    private ObjectPool<Bullet> _bulletPool;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPos;
    [SerializeField] private float _minShootDistance;
    [SerializeField] private bool _canShoot = false;
    [field: SerializeField] public float LifeTime { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }



    [Inject]
    private DiContainer _container;
    private void Awake()
    {
        _bulletPool = new ObjectPool<Bullet>(CreateBullet, GetBullet, ReleaseBullet);
        Waypoint.OnPointEnter += EnableShooting;
        Waypoint.OnPointComplete += DisableSooting;
    }

    private void Update()
    {
        if (_canShoot && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 direction = hit.point - _bulletSpawnPos.position;
                if (direction.magnitude > _minShootDistance)
                {
                    var bullet = _bulletPool.Get();
                    bullet.transform.position = _bulletSpawnPos.position;
                    bullet.Launch(direction);
                }
            }
        }
    }

    private Bullet CreateBullet()
    {
        var bullet = _container.InstantiatePrefabForComponent<Bullet>(_bulletPrefab, transform);
        bullet.SetPool(_bulletPool);
        return bullet;
    }

    private void GetBullet(Bullet bullet) =>
        bullet.gameObject.SetActive(true);

    private void ReleaseBullet(Bullet bullet) =>
        bullet.gameObject.SetActive(false);

    private void EnableShooting() =>
        _canShoot = true;

    private void DisableSooting() =>
        _canShoot = false;
}
