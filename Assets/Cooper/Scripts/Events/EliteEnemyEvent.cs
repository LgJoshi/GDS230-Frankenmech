using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemyEvent : MonoBehaviour
{
    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Player"))
        {
            print("An elite enemy has appeared!");
        }
    }
}
