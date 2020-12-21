using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombat
{
    void TakeDamage(float inDamage);
    void DealDamage(float inDamage);
}
