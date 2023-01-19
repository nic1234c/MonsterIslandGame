using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerHealth : MonoBehaviour
{
	public int maxHealth;
	public int currentHealth;
	public Healthbar healthBar;
	public PlayerAnimator playerAnimator;
	public PlayerMove playerMove;
	public int healSpellUsed;
	InfoEvent infoEvent;
	AudioSource audioSource;
	AudioClips audioClips;

	[SerializeField] TextMeshProUGUI healSpellUsesText;

    void Start()
    {
		SetMaxHealth(100);
		playerMove = GetComponent<PlayerMove>();
		playerAnimator = GetComponentInChildren<PlayerAnimator>();
		infoEvent = GetComponent<InfoEvent>();
		audioSource = GetComponent<AudioSource>();
		audioClips = GetComponentInParent<AudioClips>();
		healSpellUsed = 0;
    }
	
	public void SetMaxHealth(int health){
		maxHealth = health;
		currentHealth = health;
		healthBar.SetMaxHealth(health);
	}
	
	public void TakeDamage(int damage)
	{
		if(!playerMove.isInvincible) {
			currentHealth = currentHealth - damage;
			playerAnimator.PlayAnimation("Hit",true);
			audioSource.clip = audioClips.GetClip("Hit");
			audioSource.Play();
			healthBar.SetCurrentHealth(currentHealth);
		}
		
		if(currentHealth <= 0 ) {
			
			playerAnimator.PlayAnimation("Death",true);
			Collider col = GetComponent<Collider>();
			col.enabled = false;
			
		} 
		else {
			Collider col = GetComponent<Collider>();
			col.enabled = true;
		}
	}
	
	public void HealAction(Spell spell) {
		
		if(spell.amountOfUses != 0) {
			currentHealth = currentHealth + spell.power;
			currentHealth = Mathf.Clamp(currentHealth,0 , maxHealth);
			playerAnimator.PlayAnimation("Heal",true);
			audioSource.clip = audioClips.GetClip("Heal");
			audioSource.Play();
			healthBar.SetCurrentHealth(currentHealth);
			GameObject attackSpell = Instantiate(spell.spell, transform.position + new Vector3(0,0,0), Quaternion.identity) as GameObject;
			StartCoroutine(DisableHealEffectSpell(attackSpell));
			
			spell.amountOfUses -= 1;
		}
		healSpellUsesText.text = (spell.amountOfUses).ToString();
			
	}
	
	public IEnumerator DisableHealEffectSpell(GameObject spell) {
		yield return new WaitForSeconds(3f);
		Destroy(spell);
	}

}
