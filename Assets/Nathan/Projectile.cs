using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int speed;
    public ParticleSystem Hiteffect;
    
    

    private void Update()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(+speed, 0, 0));

        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy")
        {
            Transform particleTransform = Instantiate(Hiteffect).transform;

            particleTransform.position = this.transform.position;

            Debug.Log("Hit");

            die();
            
            
        }
    }


    void die()
    {
        Destroy(gameObject);
    }


}
