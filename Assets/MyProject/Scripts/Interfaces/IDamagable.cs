using UnityEngine;
using System;

public interface IHeal
{
    public int Health { get; }

    public EventHandler<int> TakeDmg { get; }
    public EventHandler<int> TakeHeal { get; }
    //public void Die();
}
