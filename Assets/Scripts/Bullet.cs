using UnityEngine;
using UnityEngine.Pool;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private IObjectPool<Bullet> _pool;
    private Rigidbody _bulletRb;
    private IShooting _shooting;
    private float _disableTime;

    [Inject]
    public void Construct(IShooting shooting) =>
        _shooting = shooting;

    private void Awake() =>
        _bulletRb = GetComponent<Rigidbody>();

    private void Update()
    {
        if (Time.time >= _disableTime)
            _pool.Release(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Bullet bullet))
        {
            if (other.TryGetComponent(out IDamageable doDamage))
                doDamage.TakeDamage(_shooting.Damage);
            
            _pool.Release(this);
        }

    }

    private void OnEnable() =>
        _disableTime = Time.time + _shooting.LifeTime;

    private void OnDisable() =>
        _bulletRb.velocity = Vector3.zero;

    public void SetPool(IObjectPool<Bullet> pool) =>
        _pool = pool;

    public void Launch(Vector3 direction) =>
        _bulletRb.velocity = direction.normalized * _shooting.Speed;

}
