using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemEvent : MonoBehaviour
{
   [SerializeField] GameObject itemEvent;
   [SerializeField] Image itemImage;
   [SerializeField] TextMeshProUGUI name;
	Item receivedItem;   
   
   public void triggerItemEvent(Item item)
   {
	    receivedItem = item;
		itemImage.sprite = receivedItem.itemIcon;
		name.text = "Recevied: " + receivedItem.itemName;
		itemEvent.SetActive(true);
		StartCoroutine(EventTime());
   }
   
   IEnumerator EventTime() {
	   yield return new WaitForSeconds(3f);
	   itemEvent.SetActive(false);
   }
}
