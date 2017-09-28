using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class GameCreate : MonoBehaviour {

    public GameController GC;
    public Map map;
    
    void Start ()
    {
        map = new GameObject("MapController").AddComponent<Map>();
        map.Load("map01.bgm");

        GameController GC = new GameObject("GameController").AddComponent<GameController>();
        GC.initMap(map);
    }

   
    void Update ()
    {
    }
}
