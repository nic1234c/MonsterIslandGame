using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Spells")]
public class Spell : Item
{
    public GameObject spell;
	public string name;
	public int uses;
	public int amountOfUses;
	public int power;
	
}
