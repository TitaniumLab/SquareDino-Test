using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Waypoint : MonoBehaviour
{
    public static event Action OnPointEnter;
    public static event Action OnPointComplete;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IGuided guided) && guided.CurrentPoint == transform)
        {
            OnPointEnter();
            Debug.Log($"Point \"{this.name}\" arrived.");
        }
    }
}
