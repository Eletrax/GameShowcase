using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour {

	public GameObject player;
	public float health = 200;
	public bool IsDead = false;

	public void PlayerDamage (float amount)
	{
		health -= amount;

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		player.SetActive(false);
		IsDead = true;
	}

}
