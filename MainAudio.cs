using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudio : MonoBehaviour
{
    [SerializeField] List<AudioClip> mainAudioClips;
    AudioSource audioSource;
	Enemy[] enemies;
	AudioClip clipToUse;
	public int clipNum = 0;
	public int prevClipNum;
	void Start()
    {
        audioSource = GetComponent<AudioSource>();
		prevClipNum = clipNum;
    }
	
	void Update() {
		enemies = GetComponentsInChildren<Enemy>();
	
			for(int i = 0; i < enemies.Length; i++) {
				if(enemies[i].triggered) {	
					clipToUse = mainAudioClips[1];
					clipNum = 1; 
					break;
				} 
				else if(i == enemies.Length - 1) {
					clipToUse = mainAudioClips[0];
					clipNum = 0;
				}
			}
		
			if(!audioSource.isPlaying) {
				prevClipNum = clipNum;
				audioSource.clip = clipToUse;
				audioSource.Play();
			} 
			else if(prevClipNum != clipNum) {
				audioSource.Stop();
			}
		
		
	} 
	
	
	

    
}
