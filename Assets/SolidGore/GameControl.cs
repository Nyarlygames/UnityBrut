using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {
    private Vector3 campos = Vector3.zero;
    public string camtarget = "Wisp";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        campos = GameObject.Find(camtarget).GetComponent<Transform>().position;
        campos.y += 50;
        campos.z -= 50; 
        Camera.main.GetComponent<Transform>().position = campos;
	}
}
