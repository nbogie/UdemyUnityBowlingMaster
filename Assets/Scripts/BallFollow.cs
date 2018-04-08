using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFollow : MonoBehaviour
{
    public GameObject objectToTrack;
    public Vector3 positionOffset;
    public float maxZInLane;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 cpos = transform.position;
        Vector3 tpos = objectToTrack.transform.position;

        float x = tpos.x + positionOffset.x;
        float y = tpos.y + positionOffset.y;
        float z = Mathf.Min(tpos.z, maxZInLane) + positionOffset.z;

        transform.position = new Vector3(x, y, z);

    }
}
