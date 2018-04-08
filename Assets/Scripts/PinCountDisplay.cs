using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCountDisplay : MonoBehaviour
{

    private Text pinCountText;
    public PinMachine pinMachine;
    public Color tentativeColor;
    public Color firmColor;

    void Start()
    {
        pinCountText = GetComponent<Text>();
    }

    void Update()
    {
        UpdatePinCount();
    }

    public void UpdatePinCount()
    {
        int n = pinMachine.CountStandingPins();
        pinCountText.text = "Pins: " + n;
    }

    public void SetTentative()
    {
        pinCountText.color = tentativeColor;
    }
    public void SetFirm()
    {
        pinCountText.color = firmColor;
    }
}
