using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon")]
public class Weapon : Item
{
    public GameObject prefab;
	public bool isUnarmed;
	
	public string lightAttack;	
	public int might;
	
}
