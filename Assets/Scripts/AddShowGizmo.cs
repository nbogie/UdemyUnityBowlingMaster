using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddShowGizmo : MonoBehaviour {

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, new Vector3(0.3f, 0.01f, 0.3f));
	}
}
