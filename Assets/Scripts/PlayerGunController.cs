using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGunController : MonoBehaviour {

    Camera fireCam;

    public Text OutOfAmmoText;

    public float ammo;
    public float currentAmmo;
    public float timeBetween;
    public float fireRate;
    private bool canThrow;
    public GameObject bullet;
    public PlayerController playerController;

	// Use this for initialization
	void Start () {
        OutOfAmmoText = FindObjectOfType<Text>();
        OutOfAmmoText.enabled = false;
        tag = "GunA";
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        canThrow = true;
        ammo = 3;
        currentAmmo = ammo;
        fireCam = Camera.main;

        fireRate = 0.15f;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        timeBetween += Time.deltaTime;
        if(Input.GetAxisRaw("Fire1") == 1 && timeBetween > fireRate)
        {
            if(currentAmmo > 0)
            {
                Shoot();
            }
            else
            {
                OutOfAmmoText.enabled = true;
            }
        }
        else
        {
            OutOfAmmoText.enabled = false;
        }

        if (Input.GetAxisRaw("Fire2") == 1&&canThrow)
        {
            gameObject.tag = "Damagable";
            playerController.Throwing(gameObject);
            canThrow = false;
        }

	}
    void Shoot()
    {
        currentAmmo -= 1;
        RaycastHit hit;
        if(Physics.Raycast(fireCam.transform.position,fireCam.transform.forward,out hit))
        {

            playerController.Throwing(Instantiate(bullet, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z), transform.rotation));
            timeBetween = 0;
        }
    }
}
