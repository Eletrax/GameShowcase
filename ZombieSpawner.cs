using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

	public GameObject zombie;
	private Vector3 RandomPosition;

	// Use this for initialization
	void Start () {
		InvokeRepeating("SpawnZombie", 1, 2);
	}
	
	// Update is called once per frame
	void Update () {
		RandomPosition.x = Random.Range(.5f, 4f);
		RandomPosition.z = Random.Range(.5f, 4f);
		RandomPosition.y = 0;
	}

	void SpawnZombie ()
	{
		Instantiate(zombie, transform.position + RandomPosition, Quaternion.identity);
	}
}
