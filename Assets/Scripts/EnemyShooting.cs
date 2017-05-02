using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShooting : MonoBehaviour {

    private float ammo;
    public float currentAmmo;
    private float timeBetween;
    private float fireRate;
    private float force;

    private GameObject bulletFired;
    public GameObject bullet;
    public GameObject player;
    public GameObject gun;
    public PunchController punchC;
    public Transform hand;

	// Use this for initialization
	void Start () {
        punchC = GameObject.FindGameObjectWithTag("Hand").GetComponent<PunchController>();
        punchC.enabled = false;
        hand = transform.FindChild("Hand");
        player = GameObject.FindGameObjectWithTag("Player");
        ammo = 5;
        currentAmmo = ammo;
        fireRate = 2;
        force = 10;
    }
	
	// Update is called once per frame
	void Update () {
        #region Updates
        gun.transform.position = hand.position;
        gun.transform.SetParent(GetComponent<EnemyController>().hand);
        gun.transform.localEulerAngles = Vector3.zero; 
        timeBetween += Time.deltaTime;
        LookAt(player.transform.position);
        #endregion

        if (currentAmmo > 0 && timeBetween > fireRate)
        {
            Shoot();   
        }
	}
    void Shoot()
    {
        Vector3 forcev = GetComponent<EnemyController>().hand.forward * force;
        bulletFired = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
        bulletFired.GetComponent<Rigidbody>().AddForce(forcev, ForceMode.Impulse);
        currentAmmo -= 1;
        timeBetween = 0;
    }

    void LookAt(Vector3 where)
    {
        transform.LookAt(where);
    }
}
