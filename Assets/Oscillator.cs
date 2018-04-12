using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

	[SerializeField] Vector3 movementVector;
	[SerializeField] float period = 2f;
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
		//todo protect against period is zero
		// set movement factor automatically
		float cycles = Time.time / period; //grows continually from 0
		const float tau = Mathf.PI * 2;// about 6.28
		float rawSineWave = Mathf.Sin(cycles*tau);
		
		movementFactor = rawSineWave / 2f + 0.5f;
		Vector3 offset = movementVector * movementFactor;
		transform.position = startingPos+ offset;
	}
}
