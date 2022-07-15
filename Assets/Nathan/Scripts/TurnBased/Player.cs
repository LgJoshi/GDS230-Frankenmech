using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name;

    public int Level;

    public int damage;

    public int maxHealth;

    public int currHealth;

    public bool Stunned;

    


    public bool TakeDamage(int dmg)
    {
        currHealth -= dmg;

        if (currHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

  public void heal(int amount)
    {
        currHealth += amount;
        if(currHealth >= 100)
        {
            currHealth = maxHealth;
        }



    }




}
