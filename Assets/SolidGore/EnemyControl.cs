using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
       // Debug.Log("navmesh =>" + GetComponent<NavMeshAgent>().destination + " / " + GameObject.Find("Player").GetComponent<Transform>().position + " <= player");
        //gameObject.GetComponent<NavMeshAgent>().destination = GameObject.Find("Player").GetComponent<Transform>().position;
        //Debug.Log("navmesh =>" + GetComponent<NavMeshAgent>().destination + " / " + GameObject.Find("Player").GetComponent<Transform>().position + " <= player");

    }
}
