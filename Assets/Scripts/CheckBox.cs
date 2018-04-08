using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public PinMachine pinMachine;

    void OnTriggerExit(Collider coll)
    {
        GameObject other = coll.gameObject;

        Pin pin = other.GetComponentInParent<Pin>();
        if (pin) //lazy
        {
            Destroy(pin.gameObject, 0.1f);
        }
        else
        {
            BowlingBall ball = other.GetComponent<BowlingBall>();
            if (ball)
            {
                ball.StartRecovery();
            }
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.GetComponent<BowlingBall>())
        {
            pinMachine.BallEnteredBox();
        }
    }
}
