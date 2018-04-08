using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
    PhysicMaterial pmat;
    public float dryFloorDynamicFriction;
    public float dryFloorStaticFriction;

	void Start () {
        pmat = GetComponent<BoxCollider>().material;
	}
    void DryOutFloor()
    {
        pmat.staticFriction = dryFloorStaticFriction;
        pmat.dynamicFriction = dryFloorDynamicFriction;

    }
    void OilUpFloor()
    {
        pmat.staticFriction = 0.01f;
        pmat.dynamicFriction = 0.01f;
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
            DryOutFloor();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            OilUpFloor();
        }
	}
}
