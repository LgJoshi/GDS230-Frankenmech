using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{

    public AudioSource playSound;



    // Start is called before the first frame update
    void Start()
    {
        playSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
