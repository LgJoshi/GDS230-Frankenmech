using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string firstLevel;
    public string credits;

    SingletonDataStorage singletonDataStorage;

    // Start is called before the first frame update
    void Start()
    {
        singletonDataStorage = GameObject.Find("Singleton").GetComponent<SingletonDataStorage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        singletonDataStorage.overworldCheckpoint = 0;
        singletonDataStorage.InitializeLoadout();
        SceneManager.LoadScene(2);
    }

    public void OpenOptions()
    {

    }

    public void CloseOptions()
    {

    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(credits);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
