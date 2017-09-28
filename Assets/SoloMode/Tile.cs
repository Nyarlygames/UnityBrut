using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class Tile : MonoBehaviour {

    //GameObject object; tree, box, merchant ? as component script
    public GameObject go;
    public int x, y, z = 0;
    public string type = "";
    public bool isWall = false;
    
    void Start()
    {
    }
    
    public void initTile(string[] entries, int tilesizex, int tilesizey, int tilesizez)
    {
        int posx = 0;
        int posy = 0;
        int posz = 0;
        int scalex = 0;
        int scaley = 0;
        int scalez = 0;
        string texturepath = "";
        switch (entries[0])
        {
            case "IndestructibleWall":
                posx = int.Parse(entries[1]);
                posy = int.Parse(entries[2]);
                posz = int.Parse(entries[3]);
                scalex = int.Parse(entries[4]);
                scaley = int.Parse(entries[5]);
                scalez = int.Parse(entries[6]);
                texturepath = entries[7];
                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.AddComponent<Rigidbody>();
                go.GetComponent<Rigidbody>().isKinematic = true;
                go.name = "IndestructibleWall [" + posx + "/" + posy + "]";
                go.GetComponent<Transform>().localScale = new Vector3(scalex, scaley, scalez);
                go.GetComponent<Transform>().position = new Vector3(posx + go.GetComponent<Renderer>().bounds.size.x / 2, posy + go.GetComponent<Renderer>().bounds.size.y / 2, posz + go.GetComponent<Renderer>().bounds.size.z / 2);
                go.GetComponent<Renderer>().material.mainTexture = Resources.Load(texturepath) as Texture;
                x = posx;
                y = posy;
                z = posz;
                break;
            case "Player":
                posx = int.Parse(entries[2]);
                posy = int.Parse(entries[3]);
                posz = int.Parse(entries[4]);
                scalex = int.Parse(entries[5]);
                scaley = int.Parse(entries[6]);
                scalez = int.Parse(entries[7]);
                texturepath = entries[8];

                go = new GameObject("PlayerController" + entries[1]);

                go.AddComponent<PlayerController>();
                go.GetComponent<PlayerController>().id = int.Parse(entries[1]);
                go.GetComponent<PlayerController>().x = int.Parse(entries[2]);
                go.GetComponent<PlayerController>().y = int.Parse(entries[3]);
                go.GetComponent<PlayerController>().z = int.Parse(entries[4]);
                go.GetComponent<PlayerController>().scalex = int.Parse(entries[5]);
                go.GetComponent<PlayerController>().scaley = int.Parse(entries[6]);
                go.GetComponent<PlayerController>().scalez = int.Parse(entries[7]);
                go.GetComponent<PlayerController>().texture = entries[8];

                x = posx;
                y = posy;
                z = posz;
                break;
            default:
                break;
        }
    }
    

    void Update () {
        
    }
}
