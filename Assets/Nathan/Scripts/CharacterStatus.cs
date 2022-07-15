using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "HealthStatusData", menuName = "StatusObjects/Health", order = 1)]
public class CharacterStatus : ScriptableObject
{
    
    public int level = 1;
    public int maxHealth = 100;
    public int currHealth = 100;
    public int maxMana = 100;
    public int currMana = 100;
    public float[] position = new float[2];
    public GameObject characterGameObject;
    

    

    
}
