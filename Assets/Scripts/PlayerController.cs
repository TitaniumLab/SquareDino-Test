using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class PlayerController : MonoBehaviour, IGuided
{
    private NavMeshAgent _agent;
    [SerializeField] private Transform _startPoint;
    [Header("Parent of waypoints")]
    [SerializeField] private Transform _destinationPoints;
    private List<Transform> _points = new List<Transform>();
    private int _totalPoints;
    public Transform CurrentPoint { get; private set; }
    [SerializeField] private int _currentPointIndex = 0;

    private void Start()
    {
        Waypoint.OnPointEnter += StopToShoot;
        Waypoint.OnPointComplete += ToNextPoint;
        transform.position = _startPoint.position;
        _totalPoints = _destinationPoints.childCount;
        for (int i = 0; i < _totalPoints; i++)
        {
            _points.Add(_destinationPoints.GetChild(i));
        }
        _agent = GetComponent<NavMeshAgent>();
        SetCurrentPoint();
    }

    private void StopToShoot()
    {
        _agent.isStopped = true;
    }

    private void ToNextPoint()
    {
        _currentPointIndex++;
        if (_currentPointIndex < _totalPoints)
            SetCurrentPoint();
    }

    private void SetCurrentPoint()
    {
        CurrentPoint = _points[_currentPointIndex];
        _agent.destination = CurrentPoint.position;
    }
}
