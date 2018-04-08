using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    enum Feedback { CantNudgeWhenThrown, CantThrowWhenThrown };

    public float maxThrowSpeed;
    public float verticalThrowVariance;
    public float horizontalThrowVariance;
    public float nudgeAmount;

    private Vector3 ballStartPosition;

    private bool isThrown;
    private float chargeStartTime;
    private AudioSource audioSource;
    private Rigidbody rb;
    private PinMachine pinMachine;

    public float spinSpeed = 10000;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        isThrown = false;
        pinMachine = FindObjectOfType<PinMachine>();
        ballStartPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCharging();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            float chargeTime = Time.time - chargeStartTime;
            Debug.Log("Charging time was: " + chargeTime);
            if (chargeTime > 0)
            {
                //over-charging yields crap throw
                float throwSpeed = (chargeTime > 3) ? 0.1f : maxThrowSpeed * chargeTime / 3f;
                RandomThrowBall(throwSpeed);

            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Recover();
        }
    }
    public void StartRecovery(){
        Invoke("Recover", 2f);
    }
    public void Recover()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = ballStartPosition;
        rb.useGravity = false;
        isThrown = false;
    }
    public void Nudge(Vector3 nudgeVec)
    {
        if (isThrown){
            Debug.Log("Already thrown");    
        } else {
            rb.position += nudgeVec;
        }
    }
    public void NudgeLeft()
    {
        Nudge(Vector3.left * nudgeAmount);
    }
    public void NudgeRight()
    {
        Nudge(Vector3.right * nudgeAmount);

    }

    public void Launch(Vector3 throwVel)
    {
        if (isThrown){
            Debug.Log("already thrown ball");
            return;
        }
        rb.useGravity = true;
        rb.velocity = throwVel;

        rb.angularVelocity = Vector3.back * spinSpeed;
        Debug.Log("throw vel: " + throwVel);
        isThrown = true;
        audioSource.Play();
    }


    private void RandomThrowBall(float speed)
    {
        //we're applying one frame only of impulse, so we DON'T consider
        //Time.deltaTime.
        float up = Random.Range(-verticalThrowVariance, verticalThrowVariance);
        float rightAmt = Random.Range(-horizontalThrowVariance, horizontalThrowVariance);
        Vector3 right = Vector3.right * rightAmt;
        Vector3 throwVel = (Vector3.forward + right) * speed + Vector3.up * up;
        Launch(throwVel);
    }

    private void StartCharging()
    {
        chargeStartTime = Time.time;
    }
}
