using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerController : MonoBehaviour {

    public int id = 0;
    public int y = 0;
    public int x = 0;
    public int z = 0;
    public int scalex = 0;
    public int scaley = 0;
    public int scalez = 0;
    public string texture;
    public int control;
    public GameObject playergo;
    public Map map;
    public float speedx = 1.0f; //unused
    public float speedy = 1.0f; //unused
    public float speedz = 1.0f; //unused
    public float speed = 5.0f;
    public bool moving = false;
    public Vector3 target;
    public List<Vector3> path;
    public int[][] weights;
    public int width;
    public int height;
    public bool[,] mapnode;


    void Start ()
    {

    }

    public void setUp(int controltype)
    {

        playergo = GameObject.CreatePrimitive(PrimitiveType.Cube);
        playergo.AddComponent<PlayerGO>();
        playergo.name = "Player" + id;

        playergo.GetComponent<PlayerGO>().setUp(id, x, y, z, scalex, scaley, scalez, texture, controltype);
        control = controltype;
        map = GameObject.Find("MapController").GetComponent<Map>();


        weights = new int[map.sizez][];
        this.mapnode = new bool[map.sizex, map.sizez];
        for (int i = 0; i <map.sizez; i++)
        {
            weights[i] = new int[map.sizex];
            for (int j = 0; j < map.sizex; j++)
            {
            }
        }
    }

    void Update()
    {
        if (control == 1)
        { // mouse & keyboard 
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("hit : " + hit.point);
                    target = hit.point; // += go bound size x ?
                    target.y = playergo.transform.position.y;
                    target.z += playergo.GetComponent<Renderer>().bounds.size.z / 2;
                    float[,] tilesmap = new float[map.sizex, map.sizez];
                      for (int i = 0; i < map.sizez; i++)
                      {
                      //  string line = "";

                          for (int j = 0; j < map.sizex; j++)
                          {
                              tilesmap[j,i] = map.tilesgo[i][j].GetComponent<TileController>().weight;
                        //    line += tilesmap[j, i] + " | ";
                        }
                      //  Debug.Log("Line " + i + " : " + line);
                      }
                    Grid grid = new Grid(map.sizex, map.sizez, tilesmap);
                    //weights[z][x] = -1;

                    //weights[(int) target.z][(int) target.x] = -2;
                    // create the tiles map

                    // create a grid

                    // create source and target points
                    Vector3 _from = new Vector3(x, z ,y);
                    Vector3 _to = new Vector3(target.x, target.z, target.y);
                    path = PathFind.FindPath(grid, _from, _to);
                   // Debug.Log("Path : ");
                   // foreach (Vector3 point in path) {
                   //     Debug.Log("Point : " + point.x + " / " + point.y);
                   // }

                    //GeneratePath(map.tilesgo[z][x].GetComponent<TileController>(), map.tilesgo[(int) target.z][(int) target.x].GetComponent<TileController>());
                    //------------------------------------------------------ HERE remove path = generate and make if raycast player->target has a collider, call generate, otherwise move on. // huge perf gain, generates only if there's an obstacle. But it doesn't update colliders.                    
                    moving = true;
                }
                // else click outside map
            }
        }
        if (moving == true)
        {
            float step = speed * Time.deltaTime;
            if ((playergo.GetComponent<Transform>().position == path[0]))
            {
                if (path.Count == 1)
                {
                    path.RemoveAt(0);
                    moving = false;
                }
                else
                {
                    path.RemoveAt(0);
                }
            }
            else
            {
                Vector3 pathpoint = new Vector3(path[0].x, 0, path[0].y);
                Debug.Log("Path Point : " + pathpoint);
                playergo.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(playergo.transform.position, pathpoint, step));
            }
        }
    }

   /* void OnCollisionEnter(Collision col)
    {
        Debug.Log("collided with " + col.gameObject.name);
        if (col.gameObject.name.Contains("IndestructibleWall"))
        {
            moving = false;
            Debug.Log("collided with " + col.gameObject.name);
        }
    }*/

    
}
