using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject player;
	public Transform SpawnPoint;


	// Update is called once per frame
	void Update () {
		PlayerTarget playerTarget = player.GetComponent<PlayerTarget>();
		if (playerTarget.IsDead == true)
		{
			playerTarget.IsDead = false;
			player.transform.position = SpawnPoint.position;
			player.SetActive(true);
		}
	}
}
