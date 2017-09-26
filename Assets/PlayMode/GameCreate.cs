using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCreate : MonoBehaviour {

    public GameController GC;

	// Use this for initialization
	void Start ()
    {   var width = Random.Range((int)1, (int)10);
        var height = Random.Range((int) 1, (int) 10);
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = new Vector3 (width, height, 5);
        GameController GC = plane.AddComponent<GameController>();
        GC.ground = plane;
    }
    

    // Update is called once per frame
    void Update ()
    {
    }
}
