using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour,IDamageable
{
	[SerializeField]
	private float CurrentHealth;
	public float health => CurrentHealth;
	[SerializeField]
	private float TotalHealth;
	public float MaxHealth => TotalHealth;
	protected virtual void Start()
    {
		CurrentHealth = MaxHealth;
    }


	protected virtual void Update()
    {
        
    }

	public virtual void Destroy()
	{
		Destroy(gameObject);
	}

	public virtual void TakeDamage(float Damage)
	{
		CurrentHealth -= Damage;
		if(CurrentHealth<=0)
		{
			Destroy();
		}	
	}

	public virtual void Heal(float healValue)
	{
		if (CurrentHealth < MaxHealth)
		{
			CurrentHealth = Mathf.Clamp(CurrentHealth + healValue, 0, MaxHealth);
		}
	}

}
