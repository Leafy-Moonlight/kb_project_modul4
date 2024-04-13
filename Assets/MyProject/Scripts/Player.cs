using UnityEngine;

public class Player : MonoBehaviour, IDamageble
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _speed;

    private Vector3 _input;
    private Camera _camera;

    private int _health;

    public int Health => _health;

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

    public void Heal(int heal)
    {
        if (_health < _maxHealth)
            _health += heal;

        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    public void TakeDmg(int damage)
    {
        if (_health > damage)
            _health -= damage;
        else
        {
            _health = 0;
            Die();
        }
    }

    #endregion

}
