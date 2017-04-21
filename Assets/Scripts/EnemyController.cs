using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    NavMeshAgent nav;
    public Transform hand;
    GameObject player;
    GameObject[] pickups;
    GameObject objectHolding;
    Vector3 closestPos;
    EnemyShooting shooting;

    private float range;
    private float speed;
    private float strength;
    private bool pickingUp;
    private bool hasThrowable;

	// Use this for initialization
	void Start () {
        hand = transform.FindChild("Hand");
        hasThrowable = false;
        pickingUp = false;
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        shooting = GetComponent<EnemyShooting>();

        strength = 5;
        range = 20;
        speed = 3;
        nav.speed = speed; 
	}
	
	// Update is called once per frame
	void Update () {
        #region Updates
        hand.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        if(hasThrowable)
        {
            RaycastHit hit;
            if (objectHolding.CompareTag("Throwable") == true)
            {
                Physics.Raycast(hand.position, hand.forward, out hit, range);
                if (hit.transform.CompareTag("Player"))
                {
                    Throw(objectHolding);
                }
            }
        }

        #endregion
        #region Selecting Obj
        if (GameObject.FindGameObjectWithTag("Gun") != null&& !hasThrowable)
        {
            pickups = GameObject.FindGameObjectsWithTag("Gun");
            closestPos = pickups[0].transform.position;
            for(int i =0; i < pickups.Length; i++)
            {
                if(Vector3.Distance(pickups[i].transform.position,transform.position) < Vector3.Distance(closestPos, transform.position))
                {
                    closestPos = pickups[i].transform.position;
                    closestPos = new Vector3(closestPos.x, 1, closestPos.z);
                    transform.LookAt(pickups[i].transform.position);
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                    nav.SetDestination(closestPos);
                }
            } 
        }
        else if(GameObject.FindGameObjectWithTag("Throwable") != null && !hasThrowable)
        {
            pickups = GameObject.FindGameObjectsWithTag("Throwable");
            closestPos = pickups[0].transform.position;
            for (int i = 0; i < pickups.Length; i++)
            {
                if (Vector3.Distance(pickups[i].transform.position, transform.position) < Vector3.Distance(closestPos, transform.position))
                {
                    closestPos = pickups[i].transform.position;
                    closestPos = new Vector3(closestPos.x, 1, closestPos.z);
                    transform.LookAt(pickups[i].transform);
                    nav.SetDestination(closestPos);
                }
            }

        }
        else
        {
            if(hasThrowable == true)
            {
                nav.stoppingDistance = 5;
            }
            else
            {
                nav.stoppingDistance = 0;
            }
            closestPos = player.transform.position;
            Vector3 look = new Vector3(player.transform.position.x, 1, player.transform.position.z);
            transform.LookAt(look);
            nav.SetDestination(player.transform.position);
        }
        nav.SetDestination(closestPos);
        #endregion

        if (pickingUp)
        {
            hasThrowable = true;
            objectHolding.transform.SetParent(transform);
            objectHolding.transform.position = hand.transform.position;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Gun"))
        {
            if(!hasThrowable)
            {
                PickUp(col.gameObject);
                GetComponent<EnemyShooting>().enabled = true;
                shooting.gun = col.gameObject;
            }
        }
        else if(col.gameObject.CompareTag("Throwable"))
        {
            if(!hasThrowable)
            {
                PickUp(col.gameObject);
            }
        }
    }

    void PickUp(GameObject Object)
    {
        if (hasThrowable == false)
        {
            hasThrowable = true;
            objectHolding = Object;
            pickingUp = true;
            hasThrowable = true;
        }
    }

    void Throw(GameObject Object)
    {
        Vector3 force = Vector3.forward * strength;
        Object.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        hasThrowable = false;
    }
}
