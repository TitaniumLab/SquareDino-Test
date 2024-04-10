using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public interface IShooting
{
    public float LifeTime { get; }
    public float Speed { get; }
    public int Damage { get; }
}
