using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour {

    bool Return;
    bool punching;

    private static float punchSpeed = 0.5f;
    private static float punchForce = 10;
    private float punchTimer;
    private static float returnSpeed = 5;
    private static float range = 1;
    private static float punchTimeLength = 0.5f;
    private static float returnTimer = 0.5f;
    private float returnTime;
    private float punchLengthTimer;

    PlayerController pc;
    GameObject player;
    Transform Hand;
	// Use this for initialization
	void Start () {
        Hand = transform.parent.transform;
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        punching = false;
        Return = false;
	}
	
	// Update is called once per frame
	void Update () {
        #region Updates
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        punchTimer += Time.deltaTime;
        if(!punching && !Return)
        {
            transform.position = Hand.position;
        }

        #endregion
        #region Punch And Return
        if (punching)
        {
            if (punchLengthTimer > punchTimeLength)
            {
                punching = false;
                Return = true;
            }
            punchLengthTimer += Time.deltaTime;
            transform.LookAt(player.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, punchForce * Time.deltaTime);
        }
        if(Return)
        {
            returnTime += Time.deltaTime;
            if(returnTime > returnTimer)
            {
                Return = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, Hand.position, returnSpeed * Time.deltaTime);
        }
        #endregion
        if(Vector3.Distance(Hand.position,transform.position) > 0.5)
        {
            transform.position = Hand.position;
            punchTimer = 0;
        }        

    }

    public void Punch()
    {
        Debug.Log("Punched");
        Return = false;
        transform.LookAt(player.transform.position);
        punching = true;
    }

    void OnCollisionEnter(Collision col)
    {
        punchTimer = 0;
        if(punching&&col.gameObject.CompareTag("Player"))
        {
            pc.health -= 1;
            punching = false;
            Return = true;
            punchTimer = 0;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && punchTimer > punchSpeed)
        {
            Punch();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            transform.LookAt(player.transform.position);
        }

        if (col.gameObject.CompareTag("Player")&&punchTimer > punchSpeed)
        {
            Punch();
        }
        transform.LookAt(player.transform.position);
    }
}
