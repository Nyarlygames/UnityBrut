using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    
    public Map map;
    public GameObject main_camera;


	void Start () {
    }
    
    public void initMap(Map newmap)
    {
        map = newmap;
        main_camera = GameObject.Find("MainCamera");
        main_camera.transform.position = new Vector3(map.ground.GetComponent<Renderer>().bounds.size.x/2, ((map.sizey * map.tilesizey)/2), -(map.ground.GetComponent<Renderer>().bounds.size.z / map.sizey) *5);
        main_camera.transform.LookAt(new Vector3(map.ground.GetComponent<Renderer>().bounds.size.x / 2, 0, (map.ground.GetComponent<Renderer>().bounds.size.z / map.sizez) * 5));
    }

    void Update () {
   }
}
