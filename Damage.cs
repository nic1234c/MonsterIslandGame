using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
   Collider damage;
   
   public int currentDamage = 25;
   
   private void Awake(){
		damage = GetComponent<Collider>();
		damage.gameObject.SetActive(true);
		damage.isTrigger = true;
		damage.enabled = false;		
   }
   
   public void EnableDamage() {
	 damage.enabled = true;
   }
   
   public void DisableDamage() {
	 damage.enabled = false;
   }
   
   private void DamagePlayer(Collider collision) {
	  PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

		if(playerHealth != null)
		{
			playerHealth.TakeDamage(currentDamage);
		} 
   }
   
   private void DamageEnemy(Collider collision) {
		Enemy enemy = collision.GetComponent<Enemy>();
		PlayerLevelAttributes playerLevelAttributes = GameObject.Find("GameController").GetComponent<PlayerLevelAttributes>();
		Inventory playerInventory = GameObject.Find("GameController").GetComponent<Inventory>();

		if(enemy != null)
		{
			if(gameObject.tag == "Magic") {
				enemy.TakeDamage(playerLevelAttributes.currentSorcery + playerInventory.attackSpell.power);
			} 
			else {
				enemy.TakeDamage(playerLevelAttributes.currentStrength + playerInventory.weapon.might);
			}
		} 
   }
   
   private void OnTriggerEnter(Collider collision)
   {   
		if(collision.tag == "Player")
		{
			DamagePlayer(collision);
		}
		
		if(collision.tag == "Enemy")
		{
			DamageEnemy(collision);
		}
   }
   
   
   
}
