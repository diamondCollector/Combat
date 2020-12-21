using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICombat
{
    [SerializeField] float healthPoints;

    public void DealDamage(float inDamage)
    {

    }

    public void TakeDamage(float inDamage)
    {
        healthPoints -= inDamage;
        Debug.Log(this.name + " health: " + healthPoints);
    }
}

