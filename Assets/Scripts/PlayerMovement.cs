using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour, IMoveble
{
    private NavMeshAgent _agent;
    [SerializeField] private Transform _startPoint;
    [Header("Parent of waypoints")]
    [SerializeField] private Transform _destinationPoints;
    private List<Transform> _points = new List<Transform>();
    private int _totalPoints;
    public Transform CurrentPoint { get; private set; }
    [SerializeField] private int _currentPointIndex = 0;
    [SerializeField] private float _arriveRotationTime = 0.5f;
    private Tween _tween;
    public event Action OnMove;
    public event Action OnStop;

    private void Start()
    {
        Waypoint.OnPointEnter += StopToShoot;
        Waypoint.OnPointComplete += ToNextPoint;
        GameController.OnStart += SetCurrentPoint;
        transform.position = _startPoint.position;
        _totalPoints = _destinationPoints.childCount;
        for (int i = 0; i < _totalPoints; i++)
            _points.Add(_destinationPoints.GetChild(i));

        _agent = GetComponent<NavMeshAgent>();
    }


    private void OnDestroy()
    {
        Waypoint.OnPointEnter -= StopToShoot;
        Waypoint.OnPointComplete -= ToNextPoint;
        GameController.OnStart -= SetCurrentPoint;
        _tween?.Kill();
    }

    private void StopToShoot()
    {
        _agent.isStopped = true;
        _tween?.Kill(true);
        OnStop();
        _tween = transform.DORotate(CurrentPoint.transform.rotation.eulerAngles, _arriveRotationTime);
    }

    private void ToNextPoint()
    {
        _currentPointIndex++;
        _agent.isStopped = false;

        if (_currentPointIndex < _totalPoints)
            SetCurrentPoint();
    }

    private void SetCurrentPoint()
    {
        CurrentPoint = _points[_currentPointIndex];
        _agent.destination = CurrentPoint.position;
        OnMove();
    }
}
