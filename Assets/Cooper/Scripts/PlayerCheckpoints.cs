using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoints : MonoBehaviour
{
    public GameObject[] checkpoints;
    int current = 0;
    float rotSpeed;
    public float speed;
    float WPradius = 1;

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
