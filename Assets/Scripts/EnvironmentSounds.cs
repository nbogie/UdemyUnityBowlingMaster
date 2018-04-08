using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSounds : MonoBehaviour {
    private AudioSource audioSource;
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.Play();
	}
	
	void Update () {
		
	}
}
