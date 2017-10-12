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
    public List<List<GameObject>> tilesgo;
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

    void Start ()
    {
        /* playergo = GameObject.CreatePrimitive(PrimitiveType.Cube);
         playergo.AddComponent<PlayerGO>();
         playergo.name = "Player" + id;

         playergo.GetComponent<PlayerGO>().setUp(id, x, y, z, scalex, scaley, scalez, texture, controltype);*/
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
                                    tilesgo = new List<List<GameObject>>(); // tilesgo[z][x] and y=z
                                    for (int i = 0; i < sizez; i++)
                                    {
                                        tilesgo.Add(new List<GameObject>());
                                        for (int j = 0; j < sizex; j++)
                                        {
                                            tilesgo[i].Add(new GameObject("Tile [" + i + ":" + j + "]"));
                                            tilesgo[i][j].AddComponent<TileController>();
                                            tilesgo[i][j].GetComponent<TileController>().x = j;
                                            tilesgo[i][j].GetComponent<TileController>().y = 0;
                                            tilesgo[i][j].GetComponent<TileController>().z = i;
                                        }
                                    }
                                    break;
                                case "TileSize": // x, y, z
                                    tilesizex = int.Parse(entries[1]);
                                    tilesizey = int.Parse(entries[2]);
                                    tilesizez = int.Parse(entries[3]);
                                    if ((sizex > 0) && (sizey > 0) && (sizez > 0))
                                    {
                                        if (backgroundpic != "")
                                        {
                                            ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
                                            ground.name = "Ground";
                                            ground.GetComponent<Renderer>().material.mainTexture = Resources.Load(backgroundpic) as Texture;
                                            ground.GetComponent<Transform>().localScale = new Vector3((tilesizex / ground.GetComponent<Renderer>().bounds.size.x) * sizex, 1, (tilesizez * sizez) / ground.GetComponent<Renderer>().bounds.size.z);//ground.GetComponent<Renderer>().bounds.size;
                                            ground.GetComponent<Transform>().position = new Vector3(ground.GetComponent<Renderer>().bounds.size.x / 2, 0, ground.GetComponent<Renderer>().bounds.size.z / 2);
                                        }
                                    }
                                    break;
                                case "IndestructibleWall":
                                    /* if ((tilesizex > 0) && (tilesizey > 0) && (tilesizez > 0))
                                         tiles[int.Parse(entries[2])][int.Parse(entries[1])] = new Tile();
                                     tiles[int.Parse(entries[2])][int.Parse(entries[1])].initTile(entries, tilesizex, tilesizey, tilesizez); // type x y z scalex scaley scalez pic
                                     colliders.Add(tiles[int.Parse(entries[2])][int.Parse(entries[1])].go);*/

                                    /*   tilesgo[int.Parse(entries[3])][int.Parse(entries[1])] = new GameObject("Tile [" + int.Parse(entries[1]) + ":" + int.Parse(entries[3]) + "]");
                                       tilesgo[int.Parse(entries[3])][int.Parse(entries[1])].AddComponent<Tile>();*/
                                    tilesgo[int.Parse(entries[3])][int.Parse(entries[1])].GetComponent<TileController>().initTile("IndestructibleWall", entries, tilesizex, tilesizey, tilesizez, sizex*sizez);
                                    for (int i = 0; i < int.Parse(entries[6]); i++)
                                    {
                                        for (int j = 0; j < int.Parse(entries[4]); j++)
                                        {
                                            if ((j == 0) && (i == 0))
                                            {
                                                // do nothing, first pos occupied
                                            }
                                            else
                                            {
                                                string[] fillerwall = new string[7];
                                                fillerwall[0] = "FillerWall";
                                                fillerwall[1] = (int.Parse(entries[1]) + j).ToString();
                                                fillerwall[2] = entries[2];
                                                fillerwall[3] = (int.Parse(entries[3]) + i).ToString();
                                                fillerwall[4] = entries[4];
                                                fillerwall[5] = entries[5];
                                                fillerwall[6] = entries[6];
                                                tilesgo[(int.Parse(entries[3])) + i][(int.Parse(entries[1])) + j].GetComponent<TileController>().initTile("Filler", fillerwall, tilesizex, tilesizey, tilesizez, sizex * sizez);
                                            }
                                        }
                                    }
                                    colliders.Add(tilesgo[int.Parse(entries[3])][int.Parse(entries[1])].GetComponent<TileController>().go);
                                    break;
                                case "Player":
                                    tilesgo[int.Parse(entries[4])][int.Parse(entries[2])].GetComponent<TileController>().initTile("Player", entries, tilesizex, tilesizey, tilesizez, sizex * sizez);

                                      for (int i = 0; i < int.Parse(entries[7]); i++)
                                      {
                                          for (int j = 0; j < int.Parse(entries[5]); j++)
                                          {
                                              if ((j == 0) && (i == 0))
                                              {
                                                  // do nothing, first pos occupied
                                              }
                                              else
                                              {
                                                  string[] fillerwall = new string[7];
                                                  fillerwall[0] = "FillerPlayer";
                                                  fillerwall[1] = (int.Parse(entries[2]) + j).ToString();
                                                  fillerwall[2] = entries[2];
                                                  fillerwall[3] = (int.Parse(entries[4]) + i).ToString();
                                                  fillerwall[4] = entries[4];
                                                  fillerwall[5] = entries[5];
                                                  fillerwall[6] = entries[6];
                                                  tilesgo[(int.Parse(entries[4])) + i][(int.Parse(entries[2])) + j].GetComponent<TileController>().initTile("Filler", fillerwall, tilesizex, tilesizey, tilesizez, sizex * sizez);
                                              }
                                          }
                                      }
                                    //TileController test = tilesgo[int.Parse(entries[4])][int.Parse(entries[2])];
                                    players.Add(tilesgo[int.Parse(entries[4])][int.Parse(entries[2])].GetComponent<TileController>().go);


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
    
        }
    }
    

    void Update () {
    }
}
