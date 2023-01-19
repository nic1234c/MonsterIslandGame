using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedItemController : MonoBehaviour
{
  
   EquipedItem equipedItem;
   Damage damage;
   [SerializeField] bool usePreset;
   
   private void Awake() {
		EquipedItem item = GetComponentInChildren<EquipedItem>();
		equipedItem = item;
		SetDamage();
   }
   
   public void LoadEquipped(Weapon weapon){
		if(!usePreset)	{
			equipedItem.LoadItem(weapon);
			SetDamage();
		}
   }

   private void SetDamage()
   {
	  damage = equipedItem.currentWeaponModel.GetComponentInChildren<Damage>();
   }

   public void ActivateDamage() {
		damage.EnableDamage();
   }
   
   public void DeactivateDamage() {
	   damage.DisableDamage();
   }

}
