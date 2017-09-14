using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject target;
    public float smoothTime = 0.3f;

    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;
	// Use this for initialization
	void Start () {
        offset = transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 targetPos = target.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        //transform.position = target.transform.position + offset;
	}
}
