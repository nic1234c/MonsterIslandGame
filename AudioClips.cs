using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClips : MonoBehaviour
{
    [SerializeField] List<AudioClip> clips;
     

	public AudioClip GetClip(string clip) {
		AudioClip retVal = null;
			switch (clip)
			{
				case "Attack":
					retVal = clips[0];
					break;
				case "Hit":
					retVal = clips[1];
					break;
				case "Dodge":
					retVal = clips[2];
					break;
				case "Movement":
					retVal = clips[3];
					break;
				case "Heal":
					retVal = clips[4];
					break;
				case "FireSpell":
					retVal = clips[5];
					break;
				case "IceSpell":
					retVal = clips[6];
					break;
				case "ElecSpell":
					retVal = clips[7];
					break;
				default:
					break;
			}
		return retVal;
	}                
}
