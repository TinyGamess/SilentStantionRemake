﻿using UnityEngine;

public class HeadbobSystem : MonoBehaviour {
	
	[Range(0.001f, 0.01f)]
	public float Amount = 0.002f;
	
	[Range(1f, 30f)] 
	public float Frequency = 10.0f;
	    
	[Range(10f, 100f)] 
	public float Smooth = 10.0f;
	    
	Vector3 StartPos;

	void Start() {
		StartPos = transform.localPosition;
    }

	void Update() {
		CheckForHeadbobTrigger();
		StopHeadbob();
    }
    
	private void CheckForHeadbobTrigger() {
		Vector2 movementInput = Input.GetAxis("Horizontal") * Vector2.right + Input.GetAxis("Vertical") * Vector2.up;
		float inputMagnitude = movementInput.magnitude;
	        
		if (inputMagnitude > 0) {
			StartHeadBob();
		}
	}
        
	private Vector3 StartHeadBob() {
		Vector3 pos = Vector3.zero;
		pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * Frequency) * Amount * 1.4f, Smooth * Time.deltaTime);
		pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * Frequency / 2f) * Amount * 1.6f, Smooth * Time.deltaTime);
		transform.localPosition += pos;
		    
		return pos;
	}
	
	private void StopHeadbob() {
		if (transform.localPosition == StartPos) return;
		transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, 1 * Time.deltaTime);
	}
}
