using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoEvent : MonoBehaviour
{
   [SerializeField] GameObject infoEvent;
   [SerializeField] TextMeshProUGUI name;  
   
   public void triggerEvent(string text)
   {
		infoEvent.SetActive(true);
		name.text = text;
		StartCoroutine(EventTime());
   }
   
   IEnumerator EventTime() {
	   yield return new WaitForSeconds(3f);
	   infoEvent.SetActive(false);
   }
}
