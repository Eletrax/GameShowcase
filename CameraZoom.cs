using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

	public float fieldOfView = 60f;

	// Use this for initialization
	void Start () {
		Camera.main.fieldOfView = fieldOfView;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(1))
		{
			Camera.main.fieldOfView = 30f;
		} else
		{
			Camera.main.fieldOfView = fieldOfView;
		}
	}
}
