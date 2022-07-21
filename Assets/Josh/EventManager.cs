using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void CardCombatDelegate();
    public static event CardCombatDelegate CardDrawEvent;
    static public void CardDrawFunction()
    {
        //card behaviour uses this but might be useless
        CardDrawEvent();
    }

    public delegate void CardPlayDelegate(int input);
    public static event CardPlayDelegate CardPlayedEvent;
    static public void CardPlayedFunction(int input)
    {
        CardPlayedEvent(input);
    }
}
