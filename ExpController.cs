using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour
{
	public LevelBar levelBar;
	public int currentExp = 0;
	
	
	Enemy[] enemies;
	Inventory playerInventory;
	ItemEvent itemEvent;
    void Start()
    {
        SetExp(100);
		Enemy[] enemies = GetComponentsInChildren<Enemy>();
		playerInventory = GetComponentInParent<Inventory>();
		itemEvent = GetComponent<ItemEvent>();
    }

    void Update()
    {
		enemies = GetComponentsInChildren<Enemy>();
		for(int i = 0; i < enemies.Length; i++) {
			if(enemies[i].IsDefeated) {
				currentExp += enemies[i].expGiven;
				
				SetCurrentExp(currentExp);	
				if(enemies[i].hasDropItem) {
					playerInventory.AddToInventory(enemies[i].dropItem);
					itemEvent.triggerItemEvent(enemies[i].dropItem);
				}
				enemies[i].IsDefeated = false;
			}
		}

    }
	
	public void SetExp(int exp) {
		levelBar.SetMaxExpPerLevel(exp);
	}
	
	public void SetCurrentExp(int exp) {
		currentExp = exp;
		levelBar.SetCurrentExp(exp);
	}
	
	public int GetExp() {
		return currentExp;
	}
}
