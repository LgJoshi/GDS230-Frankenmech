using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCheckpoints : MonoBehaviour
{
    public GameObject[] checkpoints;
    public int current = 0;
    float rotSpeed;
    public float speed;
    float WPradius = 1;

    SingletonDataStorage singletonDataStorage;

    private void Start()
    {
        singletonDataStorage = GameObject.FindObjectOfType<SingletonDataStorage>();

        current = singletonDataStorage.overworldCheckpoint;
        current = 4;

        transform.position = checkpoints[current].transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown("space")) 
        {
            if (Vector3.Distance(checkpoints[current].transform.position, transform.position) < WPradius)
            {
                current++;
            }
            
        }
        transform.position = Vector3.MoveTowards(transform.position, checkpoints[current].transform.position, Time.deltaTime * speed);
    }


}
