using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableController : MonoBehaviour {

    PlayerController PC;
    Camera main;
    GameObject player;
    private float moveSpeed;
    private float timer;

	// Use this for initialization
	void Start () {
        tag = "ThrowableA";
        main = Camera.main;
        main.GetComponent<CameraController>().maximumY = 60f;
        moveSpeed = 5;
        player = GameObject.FindGameObjectWithTag("Player");
        PC = player.GetComponent<PlayerController>();
        main.transform.position = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
        if(GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().useGravity = false; 
        }
    }

    // Update is called once per frame
    void Update () {
        Time.timeScale = 0.2f;
        transform.eulerAngles = main.transform.eulerAngles;
        transform.Translate(Vector3.forward*Time.deltaTime*moveSpeed* 2);
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 25;
        } 
        else
        {
            moveSpeed = 5;
        }
        #region Destory
        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(gameObject);
            OnDestroy();
        }
        if (timer > 5)
        {
            Time.timeScale = 1;
            OnDestroy();
        }
        timer += Time.deltaTime;
        #endregion
    }
    void OnDestroy()
    {
        main.GetComponent<CameraController>().maximumY = 10;
        main.GetComponent<Camera_Controller>().player = GameObject.FindGameObjectWithTag("Player");
        main.GetComponent<Camera_Controller>().offset = main.GetComponent<Camera_Controller>().baseOffset;
        PC.enabled = true;
        Time.timeScale = 1;
        main.transform.position = Vector3.MoveTowards(main.transform.position, player.transform.position, 25 * Time.deltaTime);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Damagable"))
        {
            Destroy(col.gameObject);
        }
        else
        {
            OnDestroy();
            Destroy(gameObject);
        }
        /*if(col.game.CompareTag("Bullet"))
         {
         shatter
         }*/
    }

}
