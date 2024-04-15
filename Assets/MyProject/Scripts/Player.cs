using System;
using UnityEngine;

public class Player : MonoBehaviour, IHeal
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _speed;

    private int _currentHealth;
    private bool _isAlive = true;

    private Vector3 _input;
    private Camera _camera;

    private int _health;

    public int Health => _currentHealth;

    public EventHandler<int> TakeDmg => OnTakeDmg;
    public EventHandler<int> TakeHeal => OnHeal;

    #region UnityMethods

    private void Start()
    {
        _health = _maxHealth;
        _camera = Camera.main;
    }

    private void Update()
    {
        var hor = Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");

        _input = new Vector3(hor, 0, vert);

        if (_input.magnitude > 0.1f)
        {
            Vector3 movementVector = _camera.transform.TransformDirection(_input);

            movementVector.y = 0;
            movementVector.Normalize();

            transform.forward = movementVector;
            movementVector += Physics.gravity;

            _controller.Move(movementVector * Time.deltaTime * _speed);
        }

        _animator.SetFloat("Speed", _controller.velocity.magnitude);
    }

    #endregion

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

    private void OnTakeDmg(object sender, int damage)
    {
        if (sender is not Attacker)
            return;

        if (_currentHealth > damage)
            _currentHealth -= damage;
        else if (_isAlive)
        {
            _isAlive = false;
            _currentHealth = 0;
            Die();
        }
    }

    #endregion

}
