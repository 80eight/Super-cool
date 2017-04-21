using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour {

    public GameObject player;
    
    public Vector3 offset;
    public Vector3 baseOffset;
	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        offset = player.transform.position - transform.position;
        baseOffset = offset;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position - offset;
    }
}
