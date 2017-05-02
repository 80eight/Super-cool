using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDetector : MonoBehaviour {

    GameObject[] Enemies;
    int numberOfEnemies;
    public int enemiesAlive;
    public Text EndOfRound;


	// Use this for initialization
	void Start () {
        EndOfRound.enabled = false;
        Enemies = GameObject.FindGameObjectsWithTag("Damagable");

        numberOfEnemies = Enemies.Length;
        enemiesAlive = numberOfEnemies;
	}
	
	// Update is called once per frame
	void Update () {
        if(enemiesAlive <= 0)
        {
            EndOfRound.text = "Noice";
            EndOfRound.enabled = false;
        }
    }
}
