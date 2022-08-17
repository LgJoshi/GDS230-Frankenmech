using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCheckpoints : MonoBehaviour
{
    public GameObject[] checkpoints;
    [SerializeField] int current = 0;
    float rotSpeed;
    public float speed;
    float WPradius = 1;

    SingletonDataStorage singletonDataStorage;

    private void Start()
    {
        singletonDataStorage = GameObject.FindObjectOfType<SingletonDataStorage>();

        current = singletonDataStorage.overworldCheckpoint;

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

    public void LoadCombat(int input)
    {
        singletonDataStorage.enemyType = input;

        singletonDataStorage.overworldCheckpoint = current;

        SceneManager.LoadScene(1);
    }
}
