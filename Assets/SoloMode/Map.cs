using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class Map : MonoBehaviour {

    public GameObject ground;
    public List<GameObject> players = new List<GameObject>();
    public List<GameObject> colliders = new List<GameObject>();
    public string filename = "";
    public string backgroundpic = "";
    public Tile[][] tiles;
    public int sizex = 0;
    public int sizey = 0;
    public int sizez = 0;
    public int tilesizex = 0;
    public int tilesizey = 0;
    public int tilesizez = 0;
    
    //public Player[] players;
    //public Enemy[] enemies;
    //public Object[] objects;
    //public Merchant[] merchants;

    void Start () {
    }
    
    public void Load(string fileName)
    {
        string line;
        string[] entries;
        filename = fileName;

        try
         {
            StreamReader theReader = new StreamReader(fileName, Encoding.Default);
            using (theReader)
            {
                do
                {
                    line = theReader.ReadLine();

                    if (line != null)
                    {
                        entries = line.Split(' ');
                        if (entries.Length > 0)
                        {
                            switch (entries[0])
                            {
                                case "BG":
                                    backgroundpic = entries[1];
                                    break;
                                case "Size": //x z, height
                                    sizex = int.Parse(entries[1]);
                                    sizey = int.Parse(entries[2]);
                                    sizez = int.Parse(entries[3]);
                                    tiles = new Tile[sizey][];
                                    for (int i = 0; i < sizey; i++)
                                    {
                                        tiles[i] = new Tile[sizex];
                                    }
                                    break;
                                case "TileSize": // x, y, z
                                    tilesizex = int.Parse(entries[1]);
                                    tilesizey = int.Parse(entries[2]);
                                    tilesizez = int.Parse(entries[3]);
                                    if ((sizex >0) && (sizey > 0) && (sizez > 0))
                                    {
                                        if (backgroundpic != "")
                                        {
                                            ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
                                            ground.name = "Ground";
                                            ground.GetComponent<Renderer>().material.mainTexture = Resources.Load(backgroundpic) as Texture;
                                            ground.GetComponent<Transform>().localScale = new Vector3((tilesizex / ground.GetComponent<Renderer>().bounds.size.x)*sizex, 1, (tilesizez * sizez) / ground.GetComponent<Renderer>().bounds.size.z);//ground.GetComponent<Renderer>().bounds.size;
                                            ground.GetComponent<Transform>().position = new Vector3(ground.GetComponent<Renderer>().bounds.size.x / 2, 0, ground.GetComponent<Renderer>().bounds.size.z / 2);
                                        }
                                    }
                                    break;
                                case "IndestructibleWall":
                                    if ((tilesizex > 0) && (tilesizey > 0) && (tilesizez > 0))
                                        tiles[int.Parse(entries[2])][int.Parse(entries[1])] = new Tile();
                                    tiles[int.Parse(entries[2])][int.Parse(entries[1])].initTile(entries, tilesizex, tilesizey, tilesizez); // type x y z scalex scaley scalez pic
                                    colliders.Add(tiles[int.Parse(entries[2])][int.Parse(entries[1])].go);
                                    break;
                                case "Player":
                                    tiles[int.Parse(entries[3])][int.Parse(entries[2])] = new Tile();
                                    tiles[int.Parse(entries[3])][int.Parse(entries[2])].initTile(entries, tilesizex, tilesizey, tilesizez); // id x y z scalex scaley scalez pic
                                    players.Add(tiles[int.Parse(entries[3])][int.Parse(entries[2])].go);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                while (line != null);
                theReader.Close();
            }
        }
         catch (System.Exception e) // need to check using 'cause system breaks random (conflict with unity random)
         {
             Debug.Log("Map parsing failed : " + e);
        }
    }
    

    void Update () {
    }
}
