using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeEffect : MonoBehaviour
{
    [SerializeField] RectTransform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Disappear());
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.anchoredPosition += new Vector2(0.07f, 0);
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
