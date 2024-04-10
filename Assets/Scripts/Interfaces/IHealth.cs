using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public int MaxHealth { get; }
    public int CurrentHealth { get; }
    public event Action OnDeath;
    public event Action OnHealthChange;
    public void CanBeHit(bool hit);
}
