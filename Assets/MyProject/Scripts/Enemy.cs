using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IHeal
{
    [SerializeField] private int _maxHealth = 100;

    private int _health;
    private bool _IsAlive = true;

    public int Health => _health;

    public EventHandler<int> TakeDmg => OnTakeDmg;
    public EventHandler<int> TakeHeal => OnHeal;

    private void Start()
    {
        _health = _maxHealth;
        _IsAlive = _health > 0;
    }

    #region IDamagble

    public void Die()
    {
        print("Dead");
    }

    public void OnHeal(object sender, int heal)
    {
        if (_health < _maxHealth)
            _health += heal;

        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    public void OnTakeDmg(object sender, int damage)
    {
        if (_health > damage)
            _health -= damage;
        else
        {
            _IsAlive = false;
            _health = 0;
            Die();
        }

        if (_health == 0)
            print("Enemy is Die!");
        else
            return;

        print($"Health: {_health} | Dmg: {damage}"); 
        
    }

    #endregion
}
