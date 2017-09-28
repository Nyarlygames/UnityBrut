using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class Tile : MonoBehaviour {
    
    //Player Player;
    //GameObject object; tree, box, merchant ? as component script
    public GameObject go;
    public int posx, posy = 0;
    public string type = "";
    public bool isWall = false;
    
    void Start()
    {
    }
    
    public void initTile(string[] entries, int tilesizex, int tilesizey, int tilesizez)
    {
        int posx = int.Parse(entries[1]);
        int posy = int.Parse(entries[2]);
        int posz = int.Parse(entries[3]);
        int scalex = int.Parse(entries[4]);
        int scaley = int.Parse(entries[5]);
        int scalez = int.Parse(entries[6]);
        string texturepath = entries[7];

        switch (entries[0]) {
            case "IndestructibleWall":
                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.GetComponent<Transform>().localScale = new Vector3(scalex, scaley, scalez);
                go.GetComponent<Transform>().position = new Vector3(posx + go.GetComponent<Renderer>().bounds.size.x/2, posy + go.GetComponent<Renderer>().bounds.size.y / 2, posz + go.GetComponent<Renderer>().bounds.size.z / 2);
                go.GetComponent<Renderer>().material.mainTexture = Resources.Load(texturepath) as Texture;
                break;
            default:
                break;
        }
    }
    

    void Update () {
        
    }
}
