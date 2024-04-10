using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{

    [SerializeField] private CharacterController _controller;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _speed;
    private Camera _camera;
    private Vector3 _input;

    private void Start()
    {
        _camera = Camera.main;
    }

    
    void Update()
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
}
