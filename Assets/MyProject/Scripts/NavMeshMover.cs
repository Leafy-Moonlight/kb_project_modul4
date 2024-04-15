using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class NavMeshMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _range;
    [SerializeField] private float _speed;

    private Transform _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        _agent.SetDestination(_player.position);
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.position);

        if (distance <= _range)
        {
            _animator.SetFloat("Speed", 0);
            _agent.isStopped = true;
        }
        else
        {
            _agent.isStopped = false;
            _agent.SetDestination(_player.position);
            _animator.SetFloat("Speed", _agent.velocity.magnitude);
        }
    }

}
