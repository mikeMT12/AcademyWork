using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public static class EventBus 
{

    public static UnityAction<int> TakeDamage;
    public static void TriggerTakeDamage(int damage)
    {
        TakeDamage.Invoke(damage);
    }
}
