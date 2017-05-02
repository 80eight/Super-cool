using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleController : MonoBehaviour {

    Camera fireCam;

    private float ammo;
    private float currentAmmo;
    private float fireRate;
    private float timeBetween;
    private float force;
    private bool canThrow;
    private GameObject cloneBullet;

    Transform hand;
    public GameObject bullet;
    public PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        tag = "RifleA";
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        hand = playerController.hand;
        canThrow = true;
        ammo = 10;
        currentAmmo = ammo;
        fireCam = Camera.main;
        force = 10;
        fireRate = 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        timeBetween += Time.deltaTime;
        if (Input.GetAxisRaw("Fire1") == 1&& timeBetween > fireRate&&currentAmmo > 0)
        {
            Shoot();
        }
        if (Input.GetAxisRaw("Fire2") == 1 && canThrow)
        {
            gameObject.tag = "Damagable";
            playerController.Throwing(gameObject);
            canThrow = false;
        }

    }
    void Shoot()
    {
        currentAmmo -= 1;
        cloneBullet = Instantiate(bullet, transform.position, transform.rotation);
        cloneBullet.GetComponent<Rigidbody>().AddForce(hand.forward * force, ForceMode.VelocityChange);
        cloneBullet.tag = "ThrowableA";
        timeBetween = 0;
    }
}
