using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

    public float rotationSpeed;

	// Use this for initialization
	void Start () {
        rotationSpeed *= 4;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 rotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y + rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * 4);
	}
}
