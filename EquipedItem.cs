using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedItem : MonoBehaviour
{
    public Transform position;	
	public GameObject currentWeaponModel;
	
	public void UnloadItem() {
		if(currentWeaponModel != null)
		{
			currentWeaponModel.SetActive(false);
		}
	}
	public void UnloadItemAndDestroy() {
		if(currentWeaponModel != null) {
			Destroy(currentWeaponModel);
		}
	}
	
	public void LoadItem(Weapon weapon){
		
		UnloadItemAndDestroy();
		
		if(weapon == null)
		{
			UnloadItem();
			return;
		}
		
		GameObject item = Instantiate(weapon.prefab) as GameObject;
		SetPositionOfItem(item);
	}
	
	public void SetPositionOfItem(GameObject item) {
		if(item != null)
		{
			if(position != null)
			{
				item.transform.parent = position;
			}
			else {
				item.transform.parent = transform;
			}
			InitItemProperties(item);
		}
		currentWeaponModel = item;
	}
	
	public void InitItemProperties(GameObject item) {
		item.transform.localPosition = Vector3.zero;
		item.transform.localRotation = Quaternion.identity;
	}
	

	
	
}
