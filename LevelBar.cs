using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
	public Slider slider;
	
	public void SetMaxExpPerLevel(int totalExp)
	{
		slider.maxValue = totalExp;
		
	}
	public void SetCurrentExp(int currentExp)
	{
		slider.value = currentExp;
	}
	
	public int GetCurrentExp() {
		return (int)slider.value;
	}
}
