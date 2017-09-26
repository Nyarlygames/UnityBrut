using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public Profiles PlayerProf;

	// Use this for initialization
	void Start () {
        PlayerProf = gameObject.AddComponent<Profiles>();
        GameObject.Find("GreetingText").GetComponent<Text>().text = "Greetings " + PlayerProf.profilename + " ! ";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
