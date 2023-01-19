using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttackAction : MonoBehaviour
{
	PlayerAnimator playerAnimator;
	[SerializeField] TextMeshProUGUI attackSpellUsesText;
	AudioSource audioSource;
	AudioClips audioClips;
	
	private void Awake()
	{
		playerAnimator = GetComponentInChildren<PlayerAnimator>();
		audioSource = GetComponent<AudioSource>();
		audioClips = GetComponentInParent<AudioClips>();

	}
    public void HandleLightAttack(Weapon weapon)
	{
		if(!playerAnimator.GetVar("CanFollowUp")) {
			playerAnimator.PlayAnimation(weapon.lightAttack,true);
			playerAnimator.SetVar("CanFollowUp",true);
		}
		else {		
			playerAnimator.PlayAnimation("Followup",true);	
			playerAnimator.SetVar("CanFollowUp",false);
		}	
		audioSource.clip = audioClips.GetClip("Attack");
		audioSource.Play();
	}
	
	public void HandleSpellAttack(Spell spell) {
		
		if(spell.amountOfUses != 0) {
			playerAnimator.PlayAnimation("Spell",true);
			if(spell.name == "Pyro Ball") {
				audioSource.clip = audioClips.GetClip("FireSpell");
				audioSource.Play();
			} 
			else if (spell.name == "Cryo Ball") {
				audioSource.clip = audioClips.GetClip("IceSpell");
				audioSource.Play();
			}
			else if (spell.name == "Bolt Strike") {
				audioSource.clip = audioClips.GetClip("ElecSpell");
				audioSource.Play();
			}
			GameObject attackSpell = Instantiate(spell.spell, transform.position + new Vector3(0,1,0), Quaternion.identity) as GameObject;
		
			attackSpell.GetComponent<Rigidbody>().AddForce(transform.forward * 400f);
			StartCoroutine(EnableSpell(attackSpell.GetComponent<Collider>()));
			spell.amountOfUses -= 1;
		}
		
		attackSpellUsesText.text = (spell.amountOfUses).ToString();
	}
	
	public void WeaponChange() {
		playerAnimator.PlayAnimation("ChangeWeapon",true);
	}
	
	public IEnumerator EnableSpell(Collider col) {
		yield return new WaitForSeconds(0.2f);
		col.enabled = true;
	}
	
}
