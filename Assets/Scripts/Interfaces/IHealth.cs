using System;

public interface IHealth
{
    public int MaxHealth { get; }
    public int CurrentHealth { get; }
    public event Action OnDeath;
    public event Action<int> OnHealthChange;
    public void CanBeHit(bool hit);
}
