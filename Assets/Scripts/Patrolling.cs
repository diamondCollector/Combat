using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    [SerializeField] Transform[] _waypoints;
    Transform _currentWaypoint;
    int _waypointsCounter;
    [SerializeField] float stoppingDuration;
    [SerializeField] float _speed;

    void Start()
    {
        _currentWaypoint = _waypoints[_waypointsCounter % _waypoints.Length];
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentWaypoint.position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _currentWaypoint.position) < 0.1)
        {
            StartCoroutine(ChangeWaypoint(stoppingDuration));                        
        }
    }

    IEnumerator ChangeWaypoint(float stoppingDuration)
    {
        _waypointsCounter++;
        yield return new WaitForSeconds(stoppingDuration);       
        _currentWaypoint = _waypoints[_waypointsCounter % _waypoints.Length];
    }
}
