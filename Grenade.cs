using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

	public float delay = 3f;

	public float radius = 5f;
	public float force = 700f;
	public float DamageAmount = 40f;

	public GameObject explosionEffect;
	public GameObject HealthBar;
	public GameObject gernadeExplosion;
	
	float countdown;
	bool hasExploded = false;

	// Use this for initialization
	void Start () {
		countdown = delay;
	}
	
	// Update is called once per frame
	void Update () {
		countdown -= Time.deltaTime;
		if (countdown <= 0 && !hasExploded)
		{
			Explode();
			hasExploded = true;
			Destroy(gernadeExplosion, 1.8f);
		}
	}

	void Explode ()
	{ 
		gernadeExplosion = Instantiate(explosionEffect, transform.position, transform.rotation);
		
		Collider [] colliders = Physics.OverlapSphere(transform.position, radius);

		foreach (Collider nearbyObject in colliders)
		{
			ZombieTarget target = nearbyObject.GetComponent<ZombieTarget>();

			if (target != null)
			{
				target.TakeDamage(30);
			}

			EnemyHealthBar stats = nearbyObject.GetComponent<EnemyHealthBar>();

			if (stats != null)
			{
				stats.ChangeHealth(-30);
			}
		}
		

	
		Destroy(gameObject);
	}
}
