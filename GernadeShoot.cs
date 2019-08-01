using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GernadeShoot : MonoBehaviour {

	public GameObject gernadePrefab;
	public GameObject gunEnd;
	public float GernadeCurrentAmmo = 1;
	float ReloadTime = 3;

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && GernadeCurrentAmmo > 0)
		{
			ShootGernade();
			GernadeCurrentAmmo--;
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			StartCoroutine(Reloading());
		}
	}

	void ShootGernade ()
	{
		Rigidbody gernadeClone;
		gernadeClone = Instantiate(gernadePrefab, gunEnd.transform.position, gunEnd.transform.rotation).GetComponent<Rigidbody>();
		gernadeClone.velocity = transform.TransformDirection(Vector3.forward * 20);
	}

	private IEnumerator Reloading()
	{
		Debug.Log("GernadeReloading");
		yield return new WaitForSeconds(ReloadTime);
		GernadeCurrentAmmo = 1;
	}
}
