using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLaunch : MonoBehaviour
{

    public float throwMult;

    private BowlingBall bowlingBall;
    private Vector3 dragStartPos;
    private float dragStartTime;

    private void Start()
    {
        bowlingBall = GetComponent<BowlingBall>();
    }
    //NOT unity event handlers
    public void DragStart()
    {
        dragStartPos = Input.mousePosition;
        dragStartTime = Time.time;
    }
    public void DragEnd()
    {
        float dragDuration = Time.time - dragStartTime;
        Vector3 delta = Input.mousePosition - dragStartPos;
        Vector3 screenLaunchVel = throwMult * delta / dragDuration;
        Vector3 launchVel = new Vector3(screenLaunchVel.x, 0, screenLaunchVel.y);
        bowlingBall.Launch(launchVel);
    }

}
