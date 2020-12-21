using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    [SerializeField] Transform[] _waypoints;
    [SerializeField] float _attackCooldown = 1;
    [SerializeField] float _stoppingDuration;
    [SerializeField] float _speed;
    [SerializeField] float _attackDistance = 3;
    [SerializeField] float damage = 10;
    [SerializeField] float _maxAngle = 90;

    Transform _currentWaypoint;
    int _waypointsCounter;
    PlayerAttack _playerToAttack;
    bool _canAttack = true;

    void Start()
    {
        _currentWaypoint = _waypoints[_waypointsCounter % _waypoints.Length];
    }

    void Update()
    {
        if (_playerToAttack == null)
        {
            MoveToWaypoint();
        }
        else
        {
            ChasePlayer();
        }

        UpdateWaypoint();
    }

    private void UpdateWaypoint()
    {
        if (Vector3.Distance(transform.position, _currentWaypoint.position) < 0.1 && _playerToAttack == null)
        {
            StartCoroutine(ChangeWaypoint(_stoppingDuration));
        }
    }

    private void MoveToWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentWaypoint.position, _speed * Time.deltaTime);
    }

    private void ChasePlayer()
    {
        var enemyPosition = transform.position;
        var playerPosition = _playerToAttack.transform.position;
        transform.position = Vector3.MoveTowards(enemyPosition, playerPosition, _speed * Time.deltaTime);
        if (CheckingAbilityToAttack(enemyPosition, playerPosition))
        {
            Attack();
        }
    }

    private bool CheckingAbilityToAttack(Vector3 enemyPosition, Vector3 playerPosition)
    {
        return Vector3.Distance(enemyPosition, playerPosition) <= _attackDistance && Vector3.Angle(enemyPosition, playerPosition) <= _maxAngle;
    }

    private void Attack()
    {
        if (_canAttack)
        {
            _playerToAttack.TakeDamage(damage);
            Debug.Log("Attack enemy");
            StartCoroutine(AttackCooldown(_attackCooldown));
        }
    }

    IEnumerator ChangeWaypoint(float stoppingDuration)
    {
        _waypointsCounter++;
        yield return new WaitForSeconds(stoppingDuration);       
        _currentWaypoint = _waypoints[_waypointsCounter % _waypoints.Length];
    }

    IEnumerator AttackCooldown(float cooldown)
    {
        _canAttack = false;
        yield return new WaitForSeconds(cooldown);
        _canAttack = true;
        Debug.Log("Cooldown completed");
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerAttack>();
        if (player != null)
        {
            _playerToAttack = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerAttack>();
        if (player == _playerToAttack)
        {
            _playerToAttack = null;
        }
    }
}
