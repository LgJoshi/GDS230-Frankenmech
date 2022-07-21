using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class StatusManager : MonoBehaviour
{
    public bool isAttacked;
    public CharacterStatus playerStatus;
    private void OnTriggerEnter(Collider other)
    {
        if(this.playerStatus.currHealth > 0)
        {
            if(other.tag == "Enemy")
            {
                if (!isAttacked)
                {
                    isAttacked = true;
                    
                }
            }
        }
    }
}
