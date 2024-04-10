using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private Transform _bulletPrefab;
    [SerializeField] private int _bulletsPoolPrewarm = 10;
    [SerializeField] private int _maxBulletsPool = 20;

    private CustomUnirtyPool _unirtyPool;

    private void Awake()
    {
        _unirtyPool = new CustomUnirtyPool(_bulletPrefab, _bulletsPoolPrewarm, _maxBulletsPool);
        List<Transform> list = new List<Transform>();
        for (int i = 0; i < _bulletsPoolPrewarm * 20; i++)
        {
            list.Add(_unirtyPool.Get());

        }
        for (int i = 0; i < _bulletsPoolPrewarm * 20; i++)
        {
            _unirtyPool.Release(list[i]);

        }

    }


}
