using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{
	public Transform target;
	
	public Vector3 offset;
	public float zoomSpeed = 4f;
	public float minZoom = 5f;
	public float maxZoom = 15f;
	
	public float pitch;
	
	public float yawSpeed = 100f;
	
	private float currentZoom = 10f;
	private float yawInput = 0f;
	
	void Update()
	{
		currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
		currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
		
		yawInput -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
	}


    // Update is called once per frame
    void LateUpdate()
    {
		transform.position = target.position - offset * currentZoom;
		transform.LookAt(target.position + Vector3.up * -pitch, Vector3.up);
        transform.RotateAround(target.position, Vector3.up, yawInput);
    }
}
