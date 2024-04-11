using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealth
{
    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public int CurrentHealth { get; private set; }
    [SerializeField] private bool _canBeHit = false;
    public event Action OnDeath;
    public event Action<int> OnHealthChange;

    private void Awake() =>
        CurrentHealth = MaxHealth;

    public void TakeDamage(int damage)
    {
        if (damage > 0 && _canBeHit)
        {
            Debug.Log($"{this.name} take {damage}.");
            CurrentHealth -= damage;
            OnHealthChange(CurrentHealth);
            if (CurrentHealth <= 0)
            {
                OnDeath();
                CanBeHit(false);
            }
        }
    }

    public void CanBeHit(bool hit) =>
        _canBeHit = hit;
}
