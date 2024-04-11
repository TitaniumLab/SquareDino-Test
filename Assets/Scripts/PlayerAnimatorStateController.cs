using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimatorStateController : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.OnMove += Walking;
        _playerMovement.OnStop += Idol;
    }

    private void OnDestroy()
    {
        _playerMovement.OnMove -= Walking;
        _playerMovement.OnStop -= Idol;
    }

    private void Walking() =>
        _animator.SetBool("IsWalking", true);

    private void Idol() =>
        _animator.SetBool("IsWalking", false);
}
