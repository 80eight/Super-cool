using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Vars
    bool pickingUp;
    bool hasThrowable;
    bool grounded;

    Rigidbody rig;

    public Transform hand;

    public GameObject objectHolding;
    GameObject Player;
    public Camera mainCam;

    public float health;
    float moveSpeed;
    float jumpSpeed;
    float reachRange;
    #endregion

    // Use this for initialization
    void Start()
    {
        Input.simulateMouseWithTouches = true;
        hasThrowable = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        grounded = true;
        rig = Player.GetComponent<Rigidbody>();
        /*if(PlayerPrefs.HasKey("Class") != false)
        {
            if(PlayerPrefs.GetInt("Class") == 1)
            {
                Player.AddComponent<HealingFactor>();
                sprintSpeed = GetComponent<HealingFactor>().speed;
                defaultMoveSpeed = GetComponent<HealingFactor>().speed / 1.5;
            }
        }*/
        //delete this bit when uncommenting class
        health = 2;
        reachRange = 5;
        moveSpeed = 5;
        jumpSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        #region Updates
        if (pickingUp)
        {
            objectHolding.transform.position = Vector3.MoveTowards(objectHolding.transform.position, hand.position, moveSpeed);
        }
        if(health <= 0)
        {
            Die();
        }
        transform.eulerAngles = new Vector3(0f, mainCam.transform.eulerAngles.y, 0);
        #endregion
        #region movement
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(1f * Time.deltaTime * moveSpeed, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-1f * Time.deltaTime * moveSpeed, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0f, 0f, 1f * Time.deltaTime * moveSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0f, 0f, -1f * Time.deltaTime * moveSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            Vector3 force = new Vector3(rig.velocity.x, jumpSpeed,rig.velocity.z);
            rig.velocity = force;
        }
        rig.velocity = new Vector3(0, rig.velocity.y, 0);
        #endregion
        #region Pickup and Shoot
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            if (hasThrowable)
            {
                if(!objectHolding.CompareTag("GunA")&&!objectHolding.CompareTag("RifleA"))
                {
                    Throwing(objectHolding);
                }
            }
            else
            {
                    
                RaycastHit hitobject;
                if(Physics.Raycast(mainCam.transform.position,mainCam.transform.forward,out hitobject,reachRange))
                {
                    if(hitobject.transform.CompareTag("Throwable")||hitobject.transform.CompareTag("Gun")||hitobject.transform.CompareTag("Rifle"))
                    {
                        PickUp(hitobject.transform.gameObject);
                    }
                }
            }
        }
        #endregion

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            grounded = true;
        }
        else if(col.transform.CompareTag("ThrowableA"))
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            grounded = false;
        }
    }

    void PickUp(GameObject pickup)
    {
        pickup.GetComponent<Rigidbody>().isKinematic = true;
        if(pickup.CompareTag("Gun"))
        {
            pickup.GetComponent<PlayerGunController>().enabled = true;
        }
        else if(pickup.CompareTag("Rifle"))
        {
            pickup.GetComponent<RifleController>().enabled = true;
        }
        if(hasThrowable)
        {
            objectHolding.transform.SetParent(null);
            objectHolding.transform.Translate(Vector3.forward);
            objectHolding.GetComponent<Rigidbody>().useGravity = true;
            objectHolding.GetComponent<Rigidbody>().isKinematic = false;
        }
        pickup.transform.SetParent(hand);
        pickup.transform.eulerAngles = hand.eulerAngles;
        pickingUp = true;
        objectHolding = pickup;
        hasThrowable = true;
    }

    public void Throwing(GameObject Object)
    {
        mainCam.GetComponent<Camera_Controller>().player = Object;
        mainCam.GetComponent<Camera_Controller>().offset = new Vector3(0.5f, 0, 0);
        pickingUp = false;
        GetComponent<PlayerController>().enabled = false;
        Time.timeScale = 0.2f;
        hasThrowable = false;
        Object.AddComponent<ThrowableController>();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
