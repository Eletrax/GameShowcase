using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour {

	public float damage = 10f;
	public float fireRate = 15f;
	public float CurrentAmmo;
	public float MaxAmmoInClip;
	public float MaxAmmo;
	public float ReloadTime = 1f;
	public float Smooth = .5f;
	float bulletDistance;
	public float minDamage;
	public float maxDamage;
	public float dropOffStart;
	public float dropOffEnd;

	
	public Canvas Crosshair;
	public Animator anim;
	public ParticleSystem muzzleFlash;
	public Quaternion EmptyOriginalRotation;
	public GameObject Empty;
	public GameObject hitPointGB;
	public Animator animator;
	public Camera fpsCam;
	public Transform gunEnd;
	
	public float RecoilAmount = 2f;

	Vector3 Recoil;
	private float nextTimeToFire = 0f;

	private void Start()
	{
		EmptyOriginalRotation = new Quaternion(0,0,0,0);
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetButton("Fire1") && CurrentAmmo > 0 && Time.time >= nextTimeToFire && MaxAmmo > 0)
			{
			muzzleFlash.Play();
			nextTimeToFire = Time.time + 1f / fireRate;
			Shoot();
			animator.SetBool("IsShooting", true);
			CurrentAmmo--;
			}
			else
			{
			animator.SetBool("IsShooting", false);
			}

			StartCoroutine(RecoilReset());
		
		#region ADS
		if (Input.GetMouseButton(1))
		{
			fpsCam.fieldOfView = 30f;
			RecoilAmount = 0.01f;
		} else
		{
			RecoilAmount = .05f;
			fpsCam.fieldOfView = 60f;
		}

		if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha1) || Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha3))
		{
			animator.SetBool("ADS", false);
			fpsCam.fieldOfView = 60f;
			Crosshair.enabled = true;
		}
		#endregion ADS
		#region Reloading Input
		if (Input.GetKeyDown(KeyCode.R) && MaxAmmo > 0)
		{
			StartCoroutine(Reloading());
		}
		#endregion Reloading Input
	}
	void Shoot ()
	{
		
		RaycastHit hit;
		if (Physics.Raycast(fpsCam.transform.position, (fpsCam.transform.forward + UnityEngine.Random.insideUnitSphere * RecoilAmount).normalized, out hit))
		{
			bulletDistance = hit.distance;
			if (bulletDistance <= dropOffStart)
			{
				ZombieTarget target = hit.transform.GetComponent<ZombieTarget>();

				if (target != null)
				{
					target.TakeDamage(maxDamage);
				}
			}
			if (bulletDistance >= dropOffEnd)
			{
				ZombieTarget target = hit.transform.GetComponent<ZombieTarget>();

				if (target != null)
				{
					target.TakeDamage(minDamage);
				}
			}
			GameObject impactGO = Instantiate(hitPointGB, hit.point, Quaternion.LookRotation(hit.point));

			Destroy(impactGO, 2f);

			Empty.transform.Rotate(new Vector3(-.3f, 0, 0));

			

			

			EnemyHealthBar stats = hit.transform.GetComponent<EnemyHealthBar>();

			if (stats)
			{
				stats.ChangeHealth(-10);
			}

		} else
		{
			Destroy(Instantiate(hitPointGB), 2f);
		}

	}

	#region Reloading
	private IEnumerator Reloading () {

		float bulletsToLoad = MaxAmmoInClip - CurrentAmmo;
		float bulletsToDeduct = (MaxAmmo >= bulletsToLoad) ? bulletsToLoad : MaxAmmo;

		yield return new WaitForSeconds(ReloadTime);

		MaxAmmo -= bulletsToDeduct;
		CurrentAmmo += bulletsToDeduct;
	}
	#endregion Reloading

	private IEnumerator RecoilReset()
	{
		for (float duration = 1; duration >= 0; duration -= .1f)
		{
			Empty.transform.rotation = Quaternion.Slerp(Empty.transform.rotation, EmptyOriginalRotation, Smooth);

			yield return new WaitForSeconds(.01f);
		}
		
		if (Input.GetButtonUp("Fire1") || CurrentAmmo == 0)
		{
			
			for (float duration = 1; duration >= 0; duration -= .1f)
			{
				
				Empty.transform.rotation = Quaternion.Slerp(new Quaternion(0, 0, 0, 0), new Quaternion(0, 0, 0, 0), Smooth);
				
				yield return new WaitForSeconds(.01f);
				
			}
		}

	}
}
