using UnityEngine;


public class HealthSystem : MonoBehaviour
{

	public int maxHealth = 100;
	public int currentHealth;
	[SerializeField] private SoundAndMusicSystem soundSystem;
	[SerializeField] private HealthBar healthBar;


	void Start()
    {
		EventBus.TakeDamage += Damage;
		SetMaxHealth();
    }

	public void Damage(int damage)
	{
		currentHealth -= damage;
		soundSystem?.getDamage.Play();
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
