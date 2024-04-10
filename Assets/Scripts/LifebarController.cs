using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifebarController : MonoBehaviour
{
    [SerializeField] private Canvas _lifebarPrefab;
    private Transform _lifebarTransform;
    [SerializeField] private Vector3 _offset;

    private void Awake()
    {
        Vector3 totalOffset = transform.position + _offset;
        _lifebarTransform = Instantiate(_lifebarPrefab, totalOffset, Quaternion.identity, this.transform).transform;
    }

    private void LateUpdate()
    {
        _lifebarTransform.LookAt(Camera.main.transform.position);
    }
}
