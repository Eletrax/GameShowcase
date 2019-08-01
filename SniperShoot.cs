using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShoot : MonoBehaviour {

	
	public Camera fpsCam;
	public Canvas Crosshair;
	public Animator animSniper;
	public Animator animArms;
	public GameObject hitPointGB;
	public GameObject Empty;
	public float damage;
	public float range;
	public float RecoilAmount;
	public float ReloadTime;
	public float FireRate;
	public float currentAmmo;
	public float maxAmmo;
	
	private float NextTimeToFire = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time >= NextTimeToFire)
		{
			NextTimeToFire = Time.time + 1f / FireRate;
			Shoot();
			currentAmmo--;
			Empty.transform.Rotate(new Vector3(-20f, 0, 0)); 
		}

		if (Input.GetMouseButton(1))
		{
			animSniper.SetBool("IsScoped", true);
			animArms.SetBool("ADS", true);
			Crosshair.enabled = false;
			RecoilAmount = 0;
		}
		else 
		{
			RecoilAmount = .2f;
			animSniper.SetBool("IsScoped", false);
			animArms.SetBool("ADS", false);
			Crosshair.enabled = true;
		}
		
		if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha3))
		{
			fpsCam.fieldOfView = 60f;
			Crosshair.enabled = true;
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			StartCoroutine(Reload());
		}

	}

	void Shoot ()
	{
		RaycastHit hit;
		if (Physics.Raycast(fpsCam.transform.position, (fpsCam.transform.forward + UnityEngine.Random.insideUnitSphere * RecoilAmount).normalized, out hit, range))
		{
			Instantiate(hitPointGB, hit.point, Quaternion.identity);

			ZombieTarget target = hit.transform.GetComponent<ZombieTarget>();

			if (target != null)
			{
				target.TakeDamage(damage);
			}

			EnemyHealthBar stats = hit.transform.GetComponent<EnemyHealthBar>();

			if (stats)
			{
				stats.ChangeHealth(-50);
			}
		}
	}

	private IEnumerator Reload()
	{
		yield return new WaitForSeconds(ReloadTime);

		currentAmmo = maxAmmo;
	}
}
