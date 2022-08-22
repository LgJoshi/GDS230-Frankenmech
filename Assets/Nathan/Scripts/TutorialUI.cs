using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialUI;


    public GameObject[] tutorial;
    int image;


    // Start is called before the first frame update
    void Start()
    {
        image = 0;
        tutorialUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (image < 0)
        {
            image = 0;
        }

       


        if (image == 0)
        {
            tutorial[0].gameObject.SetActive(true);
        }
    }

    public void Next()
    {

        image += 1;

        if (image <= 6)
        {
            
            for (int i = 0; i < tutorial.Length; i++)
            {
                tutorial[i].gameObject.SetActive(false);
                tutorial[image].gameObject.SetActive(true);

            }

        }
        else if (image > 6)
        {
            tutorialUI.SetActive(false);
        }



       

        Debug.Log("next");

    }

    public void previous()
    {
        image -= 1;

        for(int i = 0; i <tutorial.Length; i++)
        {
            tutorial[i].gameObject.SetActive(false);
            tutorial[image].gameObject.SetActive(true);
        }

        Debug.Log("previous");
    }


}
