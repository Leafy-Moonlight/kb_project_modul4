using System;

public partial class Attacker
{
    public interface IHeal
    {
        public int Health { get; }

        public EventHandler<int> TakeDamage { get; }
        public EventHandler<int> TakeHeal { get; }
    }

}
