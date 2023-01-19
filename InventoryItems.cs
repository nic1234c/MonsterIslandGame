using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryItems : MonoBehaviour
{
   
   [SerializeField] List<Image> inventoryImages;
   Inventory inventory;
   
    void Start()
    {
        inventory = GetComponentInParent<Inventory>();
		SetInventoryItems();
    }
	
	void Update() {
		SetInventoryItems();
	}
	
	void SetInventoryItems() {
		for(int i = 0; i < inventory.items.Count; i++) {
			
			inventoryImages[i].sprite = inventory.items[i].itemIcon;
		}
		
	}
	
}
