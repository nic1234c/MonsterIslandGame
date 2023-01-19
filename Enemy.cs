using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
	public int healthLevel = 100;
	public int maxHealth;
	public int currentHealth;
	public Healthbar healthBar;
	public Collider fov;
	public bool triggered;
	public bool IsMoving;
	public bool IsDefeated;
	[SerializeField ] bool isBoss;
	Animator animator;
	AudioSource audioSource;
	AudioClips audioClips;
	[SerializeField] public bool hasDropItem;
	[SerializeField] public Item dropItem;
	
	public int expGiven = 25;
	
    void Start()
    {
		maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		animator = GetComponent<Animator>();
		fov = GetComponentInChildren<Collider>();
		audioClips = GetComponentInParent<AudioClips>();
		audioSource = GetComponent<AudioSource>();
		if(isBoss) {
			expGiven = 100;
		}
    }
	
	void Update() {
		EnemyAction();
	}
	
	private void EnemyAction() {
	
		if(triggered) {
			
			var target = GameObject.Find("PlayerModel");
			
			if (Vector3.Distance(transform.position, target.transform.position) >= 1)
			 {
				if(animator.GetBool("isInteracting"))
					 return;
				 
					transform.position += transform.forward * 7 * Time.deltaTime;
					
					animator.SetFloat("Vertical",transform.position.x);
					animator.SetFloat("Horizontal",transform.position.y);
					if (!audioSource.isPlaying) {
						audioSource.clip = audioClips.GetClip("Movement");
						audioSource.pitch = 1.5f;
						audioSource.volume = 0.05f;
						audioSource.Play (0);
					}

					Vector3 newDir = Vector3.RotateTowards(transform.forward,target.transform.position - transform.position ,3, Time.deltaTime);
					transform.rotation = Quaternion.LookRotation(newDir);
	 
				 if (Vector3.Distance(transform.position, target.transform.position) <= 2)
				 {
						int randomNumber = UnityEngine.Random.Range(0, 2);
									
						if(randomNumber == 0) {
							animator.SetBool("isInteracting", true);
							animator.CrossFade("lightAttack", 0.2f);
						} 
						else {
							animator.SetBool("isInteracting", true);
							animator.CrossFade("lightAttack2", 0.2f);
						}
						audioSource.clip = audioClips.GetClip("Attack");
						audioSource.volume = 1f;
						audioSource.Play();
				 }
	 
			 }
		} 
	}
	

	
	private void OnTriggerEnter(Collider collision) {
		if(collision.tag == "Player") {
			animator.SetBool("isTriggered", true);
			triggered = animator.GetBool("isTriggered");
			
		}
	}

	private int SetMaxHealthFromHealthLevel()
	{
		maxHealth = healthLevel * 10;
		return maxHealth;
	}
	
	public void TakeDamage(int damage)
	{
		currentHealth = currentHealth - damage;
		animator.SetBool("isInteracting", true);
		animator.CrossFade("Hit", 0.2f);
		audioSource.clip = audioClips.GetClip("Hit");
		audioSource.volume = 1f;
		audioSource.Play();
		if(isBoss){
			healthBar.SetCurrentHealth(currentHealth);
		}
		
		if(currentHealth <= 0 ) {
			animator.CrossFade("Death", 0.2f);
			Collider col = GetComponent<Collider>();
			col.enabled = false;
			IsDefeated = true;
			StartCoroutine(EnemyDisappearOnDeath());
			triggered = false;
		}
	}
	
	public IEnumerator EnemyDisappearOnDeath() {
		yield return new WaitForSeconds(3f);
		gameObject.SetActive(false);
	}

}
