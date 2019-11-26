using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<NavMeshAgent>().Warp(GetComponent<Transform>().position);

    }

    public void StartMoving()
    {
        GetComponent<NavMeshAgent>().destination = GameObject.Find("Player_Selected").GetComponent<Transform>().position;
    }

	// Update is called once per frame
	void Update ()
    {
       // Debug.Log("navmesh =>" + GetComponent<NavMeshAgent>().destination + " / " + GameObject.Find("Player").GetComponent<Transform>().position + " <= player");

        //Debug.Log("navmesh =>" + GetComponent<NavMeshAgent>().destination + " / " + GameObject.Find("Player").GetComponent<Transform>().position + " <= player");

    }
}
