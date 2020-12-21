using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float _maxDistance;
    [SerializeField] float _maxAngle;
    [SerializeField] float damage;
    List<Enemy> _currentEnemies;

    private void Start()
    {
        _currentEnemies = new List<Enemy>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < _currentEnemies.Count; i++)
            {
                if (Vector3.Distance(transform.position, _currentEnemies[i].transform.position) <= _maxDistance &&
                    Vector3.Angle(transform.position, _currentEnemies[i].transform.position) <= _maxAngle)
                {
                    _currentEnemies[i].TakeDamage(damage);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();

        if (enemy != null && !_currentEnemies.Contains(enemy))
        {
            _currentEnemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();

        if (enemy != null && _currentEnemies.Contains(enemy))
        {
            _currentEnemies.Remove(enemy);
        }
    }
}
