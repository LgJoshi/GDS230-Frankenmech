using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCheckpoints : MonoBehaviour
{
    public GameObject[] checkpoints;
    [SerializeField] int current;
    float rotSpeed;
    public float speed;
    float WPradius = 1;
    [SerializeField] GameObject playerModel;

    SingletonDataStorage singletonDataStorage;

    private void Start()
    {
        singletonDataStorage = GameObject.Find("Singleton").GetComponent<SingletonDataStorage>();
        Debug.Log(singletonDataStorage.overworldCheckpoint);
        current = singletonDataStorage.overworldCheckpoint;

        Debug.Log("player CP script start");

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
        playerModel.transform.rotation = 
            Quaternion.RotateTowards(
                playerModel.transform.rotation, 
                checkpoints[current].transform.rotation * Quaternion.Euler(0, 90, 0),
                1f
                );

    }

    public void LoadCombat(int input)
    {
        singletonDataStorage.enemyType = input;

        singletonDataStorage.overworldCheckpoint = current;

        SceneManager.LoadScene(1);
    }
}
