using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI: MonoBehaviour
{
	Inventory playerInventory;
	[SerializeField] string type;
    [SerializeField] Image icon;
	[SerializeField] TextMeshProUGUI name;
	[SerializeField] TextMeshProUGUI uses;


    void Update()
    {
       playerInventory = GetComponentInParent<Inventory>();
	   
	   if(type == "HealSpell") {
		   SetHealInventory();
	   }
	   else if(type == "AttackSpell") {
		   SetAttackSpellInventory();
	   }
	   else if(type == "Weapon") {
		   SetWeaponInventory();
	   }
    }

    private void SetHealInventory() {
	   icon.sprite = playerInventory.healSpell.itemIcon;
	   name.text = playerInventory.healSpell.name;
	   uses.text = playerInventory.healSpell.amountOfUses.ToString();
	}
	
	private void  SetAttackSpellInventory() {
	   icon.sprite = playerInventory.attackSpell.itemIcon;
	   name.text = playerInventory.attackSpell.name;
	   uses.text = playerInventory.attackSpell.amountOfUses.ToString();
		
	}
	
	private void  SetWeaponInventory() {
	   icon.sprite = playerInventory.weapon.itemIcon;
	   name.text = playerInventory.weapon.itemName;
		
	}
}
