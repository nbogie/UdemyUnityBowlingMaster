using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinMachine : MonoBehaviour
{
    public GameObject rackPrefab;
    public Animator animator;
    public GameObject pinHolder;
    public PinCountDisplay pinCountDisplay;

    private GameObject currentRack;
    private bool ballEnteredBox;
    private int lastStandingCount;
    private float lastStandingCountAt;

    void Start()
    {
        ballEnteredBox = false;
        animator = GetComponent<Animator>();

    }

    public void NewRackFloating()
    {
        GameObject rack = GameObject.FindWithTag("Rack");
        Destroy(rack);
        currentRack = Instantiate(rackPrefab, pinHolder.transform.position, Quaternion.identity, pinHolder.transform);
        LockOrUnlockRackPinsForPlacement(true);
    }

    public void ReleasePins()
    {
        if (!currentRack)
        {
            Debug.LogError("No current rack!");
            return;
        }
        currentRack.transform.parent = null;
        LockOrUnlockRackPinsForPlacement(false);
    }
    public void AffixStandingPinsToRaiser()
    {
        if (!currentRack)
        {
            Debug.LogError("No current rack!");
            return;
        }
        List<Pin> standingPins = ComputeStandingPins();

        currentRack.transform.DetachChildren();

        foreach (Pin pin in standingPins)
        {
            pin.transform.parent = currentRack.transform;
            pin.LockOrUnlockForPlacement(true);
        }
        currentRack.transform.parent = pinHolder.transform;
        Debug.Log("new rack affixed to raiser");

        LockOrUnlockRackPinsForPlacement(true);
    }

    public void DoNothingAtEndOfAnim()
    {
        Debug.Log("doing nothing at end of anim for " + name);
    }

    public void SayFromAnim(string msg)
    {
        Debug.Log("From Anim Event: " + msg);
    }
    public List<Pin> ComputeStandingPins()
    {
            
        List<Pin> standingPins = new List<Pin>();
        if (!currentRack)
        {
            return standingPins;
        }
        foreach (Transform t in currentRack.transform)
        {
            Pin pin = t.gameObject.GetComponentInChildren<Pin>();
            if (pin)
            {
                if (pin.IsStanding())
                {
                    standingPins.Add(pin);
                }
            }
        }
        return standingPins;
    }

    public void BallEnteredBox()
    {
        ballEnteredBox = true;
        recordStandingCountNow(ComputeStandingPins().Count);
        pinCountDisplay.SetTentative();
    }

    public int CountStandingPins()
    {
        if (currentRack)
        {
            return ComputeStandingPins().Count;
        }
        return -1;
    }

    private void LockOrUnlockRackPinsForPlacement(bool lockThem)
    {
        if (!currentRack)
        {
            Debug.LogError("NO CURRENT RACK - can't turn on/off gravity!");
            return;
        }
        foreach (Transform t in currentRack.transform)
        {
            Pin p = t.GetComponentInChildren<Pin>();
            if (p)
            {
                p.LockOrUnlockForPlacement(lockThem);
            }
            else
            {
                Debug.LogError("Couldn't find any pin under rack obj: " + t.gameObject.name);
            }
        }
    }


    void Update()
    {
        if (ballEnteredBox)
        {
            float timeSinceLastChange = Time.time - lastStandingCountAt;

            if (timeSinceLastChange > 3f)
            {
                PinsHaveSettled();
            }
            else
            {
                int count = CountStandingPins();
                if (count != lastStandingCount)
                {
                    recordStandingCountNow(count);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            animator.SetTrigger("tidyTrigger");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("resetTrigger");

        }
    }

    private void recordStandingCountNow(int count)
    {
        lastStandingCount = count;
        lastStandingCountAt = Time.time;
    }

    void PinsHaveSettled()
    {
        pinCountDisplay.SetFirm();
        ballEnteredBox = false;
        //TODO: ball could bounce back in...

    }
}
