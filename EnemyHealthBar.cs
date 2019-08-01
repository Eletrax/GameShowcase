using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

	public Transform healthBar;
	public Slider healthFill;
	public Canvas healthBarCanvas;

	public float healthBarYOffset = 2;

	public float currentHealth;

	public float MaxHealth;
	// Update is called once per frame
	void Update () {
		PositionHealthBar();
	}

	public void ChangeHealth(float amount)
	{
		currentHealth += amount;
		currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);

		healthFill.value = currentHealth / MaxHealth;

		if (currentHealth <= 0)
		{
			Destroy(healthBarCanvas);
		}
	}

	private void PositionHealthBar()
	{
		Vector3 currentPos = transform.position;

		healthBar.position = new Vector3(currentPos.x, currentPos.y + healthBarYOffset, currentPos.z);

		healthBar.LookAt(Camera.main.transform);
	}
}
