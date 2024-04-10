using UnityEngine;
using UnityEngine.Pool;

public class CustomUnirtyPool
{
    private ObjectPool<Transform> _pool;
    private Transform _prefab;

    public CustomUnirtyPool(Transform prefab, int prewarmCapacity, int maxCapacity)
    {
        _prefab = prefab;
        _pool = new ObjectPool<Transform>(OnCreate, OnGet, OnRelease, OnDestroy, false, prewarmCapacity);
    }

    public Transform Get()
    {
        var obj = _pool.Get();
        return obj;
    }

    public void Release(Transform obj)
    {
        _pool.Release(obj);
    }


    private void OnGet(Transform obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnRelease(Transform obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroy(Transform obj)
    {
        GameObject.Destroy(obj);
    }

    private Transform OnCreate()
    {
        return GameObject.Instantiate(_prefab);
    }
}
