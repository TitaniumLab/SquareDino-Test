using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private IHealth[] _enemiesToDefeat;
    [SerializeField] private int _defeatCount = 0;
    [SerializeField] private int _totalEnemies;
    [SerializeField] private bool _isFinalPoint = false;
    public static event Action OnPointEnter;
    public static event Action OnPointComplete;
    public static event Action OnFinalPoint;

    private void Awake()
    {
        _enemiesToDefeat = GetComponentsInChildren<IHealth>();
        _totalEnemies = _enemiesToDefeat.Length;

        foreach (var item in _enemiesToDefeat)
        {
            item.OnDeath += KillCount;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IMoveble guided) && guided.CurrentPoint == transform)
        {
            OnPointEnter();
            Debug.Log($"Point \"{this.name}\" arrived.");
            if (_isFinalPoint)
                OnFinalPoint();
            foreach (var item in _enemiesToDefeat)
            {
                item.CanBeHit(true);
            }
        }
    }

    private void KillCount()
    {
        _defeatCount++;
        if (_defeatCount == _totalEnemies)
            OnPointComplete();
    }
}
