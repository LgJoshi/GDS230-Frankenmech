using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void CombatDelegate();
    public static event CombatDelegate CardDrawEvent;
    static public void CardDrawFunction()
    {
        //card behaviour uses this but might be useless
        CardDrawEvent();
    }
    //event bfor start of player's turn
    public static event CombatDelegate PlayerTurnEvent;
    static public void PlayerTurnFunction()
    {
        //card behaviour uses this but might be useless
        PlayerTurnEvent();
    }

    public delegate void CardPlayDelegate(int input);
    public static event CardPlayDelegate CardPlayedEvent;
    static public void CardPlayedFunction(int input)
    {
        CardPlayedEvent(input);
    }
}
