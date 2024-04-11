using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    private Rigidbody[] _rbs;

    public void Awake()
    {
        _rbs = GetComponentsInChildren<Rigidbody>();
        Disable();
    }

    public void Enable()
    {
        foreach (var item in _rbs)
            item.isKinematic = false;
    }

    public void Disable()
    {
        foreach (var item in _rbs)
            item.isKinematic = true;
    }
}
