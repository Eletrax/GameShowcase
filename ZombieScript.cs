using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ZombieScript : MonoBehaviour {

	public Animator anim;
	public GameObject player;
	private NavMeshAgent nav;
	public Vector3 DistanceBetweenPlayerAndZombie;
	private AudioSource zombieAudio;
	

	// Use this for initialization
	void Awake ()
	{
		nav = GetComponent<NavMeshAgent>();
		zombieAudio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update ()
	{
		DistanceBetweenPlayerAndZombie = transform.position - player.transform.position;

		if (DistanceBetweenPlayerAndZombie.magnitude <= 30)
		{
			nav.SetDestination(player.transform.position);
			anim.SetBool("IsWalking", true);
		} else
		{
			anim.SetBool("IsWalking", false);
			nav.SetDestination(gameObject.transform.position);
		}

		//AUDIO
		if (DistanceBetweenPlayerAndZombie.magnitude <= 10)
		{
			zombieAudio.volume = .4f;
		} else if (DistanceBetweenPlayerAndZombie.magnitude <= 20)
		{
			zombieAudio.volume = .25f;
		} else if (DistanceBetweenPlayerAndZombie.magnitude <= 30)
		{
			zombieAudio.volume = .1f;
		} else
		{
			zombieAudio.volume = 0;
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (GameObject.FindGameObjectWithTag("Player"))
		{
			InvokeRepeating("Attack", .5f, 1);
			anim.SetBool("IsAttacking", true);
		}
	}

	public void OnTriggerExit(Collider other)
	{
		CancelInvoke("Attack");
		anim.SetBool("IsAttacking", false);
	}

	public void Attack()
	{
		PlayerTarget target = player.transform.GetComponent<PlayerTarget>();

		if (target != null)
		{
			target.PlayerDamage(10);
		}
	}
}
