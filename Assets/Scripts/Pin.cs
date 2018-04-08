using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private AudioSource audioSource;
    public Rigidbody rb;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

	private void OnCollisionEnter(Collision collision)
	{
        audioSource.Play();
	}

	public bool IsStanding()
    {
        return (transform.up.y > 0.97);
    }

    internal void LockOrUnlockForPlacement(bool lockThem)
    {
        rb = GetComponent<Rigidbody>();

        if (lockThem){
            rb.useGravity = false;
            rb.freezeRotation = true;
            rb.isKinematic = true;
        } else {
            rb.isKinematic = false;
            rb.freezeRotation = false;
            rb.useGravity = true;
        }
    }
}
