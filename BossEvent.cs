using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossEvent : MonoBehaviour
{
   [SerializeField] GameObject boss;
   [SerializeField] GameObject healthBar;
   [SerializeField] TextMeshProUGUI bossName;
   [SerializeField] string nameText;

   bool isOngoing;

  
   private void Update() {
	   
		if(boss.GetComponent<Enemy>().currentHealth <= 0 && isOngoing) {
			healthBar.SetActive(false);
			isOngoing = false;
		} 
   }
   
   private void OnTriggerEnter(Collider collision)
   {
		if(collision.tag == "Player" && (boss.GetComponent<Enemy>().currentHealth > 0) )
		{	
			isOngoing = true;
			Enemy enemy = boss.GetComponent<Enemy>();
			healthBar = enemy.healthBar.gameObject;
			healthBar.GetComponent<Healthbar>().SetMaxHealth(enemy.currentHealth);
			healthBar.SetActive(true);
			bossName.text = nameText;
		}
   }
}
