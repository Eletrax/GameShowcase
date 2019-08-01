using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class ZombieTarget : MonoBehaviour {

	public Animator anim;
	public GameObject Zombie;
	public float health = 50f;
	public float DieTime;
	private NavMeshAgent nav;
	public Canvas healthbar;
	public GameObject player;

	public void TakeDamage(float amount)
	{
		health -= amount;
		if (health <= 0f)
		{
			nav = GetComponent<NavMeshAgent>();
			nav.enabled = false;
			anim.SetTrigger("IsDead");
			anim.SetBool("IsWalking", false);
			StartCoroutine(Die());
		} 
	}

	

	private IEnumerator Die ()
	{
		yield return new WaitForSeconds(DieTime);
		Destroy(healthbar);
		Destroy(gameObject);
	}
		
	
}
