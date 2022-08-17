using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldEventTrigger : MonoBehaviour
{
    //1 for regular, 2 for elite, 3 for boss
    [SerializeField] int enemyType = 1;
    
    private void OnTriggerEnter( Collider trigger )
    {
        Debug.Log("collided");
        if( trigger.CompareTag("Player") )
        {
            SingletonDataStorage singletonDataStorage = GameObject.FindObjectOfType<SingletonDataStorage>();
            singletonDataStorage.enemyType = enemyType;

            singletonDataStorage.overworldCheckpoint = trigger.GetComponent<PlayerCheckpoints>().current;

            SceneManager.LoadScene(1);
        }
    }
}
