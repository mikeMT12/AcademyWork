using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public static class EventBus 
{
    /*   private EventBus()
       {

       }

       private static EventBus _instance;
       public static EventBus Instance
       {
           get
           {
               if (_instance == null)
                   _instance = new EventBus();

               return _instance;
           }
       }


       /// Player health system
       public UnityAction<int> TakeDamage;
       public UnityAction<int> TakeHeal;*/


    //public static UnityEvent<int> TakeDamage;
    public static UnityAction<int> TakeDamage;
    public static void TriggerTakeDamage(int damage)
    {
        TakeDamage.Invoke(damage);
    }
}
