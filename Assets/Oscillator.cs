using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {
	[SerializeField] Vector3 movementVector;
	//todo: remove from inspector
	[Range (0,1)] [SerializeField] float movementFactor;
	//0 for not moving, 1 for fully moving
	Vector3 startingPos;// must be stored for absolute movement
	// Use this for initialization
	void Start () {
		startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 offset = movementVector * movementFactor;
		transform.position = startingPos+ offset;
	}
}
