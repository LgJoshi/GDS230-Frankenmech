using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyEvent : MonoBehaviour
{
    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Player"))
        {
            print("A boss enemy has appeared!");
        }
    }
}
