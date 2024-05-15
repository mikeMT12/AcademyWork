using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{

	public int maxHealth = 100;
	public int currentHealth;

	[SerializeField] private HealthBar healthBar;

	/*public HealthSystem(EventBus eventBus)
	{
		eventBus.TakeDamage.AddListener(Damage);
	}*/

	// Start is called before the first frame update
	void Start()
    {
		//EventBus.Instance.TakeDamage += Damage;
		//EventBus.Instance.TakeHeal.AddListener(Heal);
		//EventBus.TakeDamage.AddListener(Damage);
		EventBus.TakeDamage += Damage;
		SetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
		/*if (Input.GetKeyDown(KeyCode.Space))
		{
			Damage(20);
		}*/
    }

	

	public void Damage(int damage)
	{
		currentHealth -= damage;
		if(currentHealth <= 0)
        {
			currentHealth = 0;
			Death();
        }
		healthBar.SetHealth(currentHealth);
	}

	public void Heal(int heal)
    {
		currentHealth += heal;
		healthBar.SetHealth(currentHealth);
	}

	public void SetMaxHealth()
    {
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	public void Death()
    {
		GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
	}

    private void OnDestroy()
    {
		EventBus.TakeDamage -= Damage;
	}
}
