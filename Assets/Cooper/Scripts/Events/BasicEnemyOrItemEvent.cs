using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyOrItemEvent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Random.Range(0, 10) > 6)
            {
                print("An item has appeared!");
            }
            else 
            {
                print("A basic enemy has appeared!");
            }
            
        }
    }
}
