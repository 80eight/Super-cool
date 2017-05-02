using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOnCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (col.gameObject.CompareTag("Damagable"))
        {
            Destroy(col.gameObject);
        }
        Destroy(gameObject);
    }
}
