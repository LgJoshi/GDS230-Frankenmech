using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyEvent : MonoBehaviour
{
    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Player"))
        {
            print("A basic enemy has appeared!");
        }
    }
}
