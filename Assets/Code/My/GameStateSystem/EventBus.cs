using UnityEngine.Events;


public static class EventBus 
{

    public static UnityAction<int> TakeDamage;
    public static void TriggerTakeDamage(int damage)
    {
        TakeDamage.Invoke(damage);
    }
}
