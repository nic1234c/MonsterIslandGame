using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	EquipedItemController equipedItemController;
    public Weapon weapon;
	public Spell attackSpell;
	public Spell healSpell;
	[SerializeField] public List<Item> items;
	[SerializeField] public List<Spell> equippedAttackSpells;
	[SerializeField] public List<Spell> equippedHealSpells;
	[SerializeField] public List<Weapon> equippedWeapons;

	int currentAttackSpell = 0;
	int currentHealSpell = 0;
	int currentWeapon = 0;

	[SerializeField] public GameObject inventoryMenu;
	public bool isOpen;
	
	private void Awake(){
		equipedItemController = GetComponentInChildren<EquipedItemController>();
		SetSpellUses();
	}
	
	private void Start()
	{
		equipedItemController.LoadEquipped(weapon);
	}
	
	public void ShowInventoryMenu() {
		inventoryMenu.SetActive(true);
		isOpen = true;
	}
	
	public void HideInventoryMenu() {
		inventoryMenu.SetActive(false);
		isOpen = false;
	}
	
	public void ChangeAttackSpell() {

		if(currentAttackSpell == equippedAttackSpells.Count - 1)
			currentAttackSpell = 0;
		else {
			currentAttackSpell++;
		}
		
		attackSpell = equippedAttackSpells[currentAttackSpell];
	}
	
	public void ChangeHealSpell() {

		if(currentHealSpell == equippedHealSpells.Count - 1)
			currentHealSpell = 0;
		else {
			currentHealSpell++;
		}
		
		healSpell = equippedHealSpells[currentHealSpell];
	}
	
	public void ChangeWeapon() {

		if(currentWeapon == equippedWeapons.Count - 1)
			currentWeapon = 0;
		else {
			currentWeapon++;
		}
		
		weapon = equippedWeapons[currentWeapon];
		StartCoroutine(ChangeEquippedWeapon(weapon));
	}
	
	public void AddToInventory(Item newItem) {
		items.Add(newItem);
		if(newItem.itemType == "HealingSpell") {
			Spell newHealSpell = (Spell)newItem;
			newHealSpell.amountOfUses = newHealSpell.uses;
			equippedHealSpells.Add(newHealSpell);	
		}
		else if(newItem.itemType == "AttackingSpell") {		
			Spell newAttackSpell = (Spell)newItem;
			newAttackSpell.amountOfUses = newAttackSpell.uses;
			equippedAttackSpells.Add(newAttackSpell);	
			
		}
	}
	
	private void SetSpellUses() {
		for(int i = 0; i < equippedAttackSpells.Count; i++) {
			equippedAttackSpells[i].amountOfUses = equippedAttackSpells[i].uses;
		}
		
		for(int i = 0; i < equippedHealSpells.Count; i++) {
			equippedHealSpells[i].amountOfUses = equippedHealSpells[i].uses;
		}
	}
	
	IEnumerator ChangeEquippedWeapon(Weapon weapon) {
		yield return new WaitForSeconds(0.5f);
		equipedItemController.LoadEquipped(weapon);
	}
	
}
