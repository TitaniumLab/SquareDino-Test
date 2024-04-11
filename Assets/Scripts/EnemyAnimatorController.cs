using UnityEngine;

[RequireComponent(typeof(RagdollHandler))]
[RequireComponent(typeof(IHealth))]
public class EnemyAnimatorController : MonoBehaviour
{
    private RagdollHandler _rdhandler;
    private IHealth _health;
    private Animator _animator;
    private void Start()
    {
        _rdhandler = GetComponent<RagdollHandler>();
        _health = GetComponent<IHealth>();
        _animator = GetComponentInChildren<Animator>();
        _health.OnDeath += DisableAnimator;
        _health.OnDeath += _rdhandler.Enable;
    }

    private void OnDestroy()
    {
        _health.OnDeath -= DisableAnimator;
        _health.OnDeath -= _rdhandler.Enable;
    }

    private void DisableAnimator()=>
        _animator.enabled = false;
}
