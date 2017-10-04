using UnityEngine;
using System.Collections.Generic;

public class PlayerGO : MonoBehaviour {

    public int id = -1;
    public int y = 0;
    public int x = 0;
    public int z = 0;
    public int scalex = 0;
    public int scaley = 0;
    public int scalez = 0;
    public string texture;
    public int control = 0;
    public float speedx = 0.0f;
    public float speedy = 0.0f;
    public float speedz = 0.0f;
    public float speed = 10.0f;
    public bool moving = false;
    public Map map;
    public Vector3 target;
    public Vector3 targetPath;
    public int[][] matrix = new int[3][];
    Vector3 resulttile = Vector3.zero;
    public List<Vector3> path;

    void Start ()
    {
         
    }

    public void setUp(int idcontroller, int x, int y, int z, int scalex, int scaley, int scalez, string texture, int controltype)
    {
        id = idcontroller;
        gameObject.GetComponent<Transform>().localScale = new Vector3(scalex, scaley, scalez);
        gameObject.GetComponent<Transform>().position = new Vector3(x + gameObject.GetComponent<Renderer>().bounds.size.x / 2, y + gameObject.GetComponent<Renderer>().bounds.size.y / 2, z + gameObject.GetComponent<Renderer>().bounds.size.z / 2);
        gameObject.GetComponent<Renderer>().material.mainTexture = Resources.Load(texture) as Texture;
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        if (id == 0)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        matrix[0] = new int[3];
        matrix[1] = new int[3];
        matrix[2] = new int[3];
        control = controltype;
        map = GameObject.Find("MapController").GetComponent<Map>();

    }

    void GetAPath(Vector3 tilestart, Vector3 bullseye, List<List<Vector3>> path, int startweight)
    {
       /* Vector3 neighbour = Vector3.zero;
        Vector3 p_tile = new Vector3((int)((gameObject.GetComponent<Transform>().position.x) / map.tilesizex),
            (int)((gameObject.GetComponent<Transform>().position.y) / map.tilesizey),
            (int)((gameObject.GetComponent<Transform>().position.z) / map.tilesizez));

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if ((i == 1) && (j == 1))
                {
                    // do nothing, player tile
                }
                else
                {
                    neighbour = new Vector3();
                    neighbour.x = (p_tile.x + i) * map.tilesizex;
                    neighbour.y = (p_tile.y) * map.tilesizey;
                    neighbour.z = (p_tile.z + j) * map.tilesizez;
                    //generated.Add(neighbour);
                    generated.Add(GetAPath(neighbour, bullseye, path, 1));
                }

            }
        }*/
    }

    List<Vector3> GeneratePath(Vector3 tilestart, Vector3 bullseye, List<Vector3> path, int startweight)
    {
        Vector3 t_tile = new Vector3((int)(target.x / map.tilesizex), (int)target.y, (int)(target.z / map.tilesizez));
        Vector3 decimalpart = new Vector3(target.x % 1, target.y % 1, target.z % 1);
        int xposition = (int)((gameObject.GetComponent<Transform>().position.x) / map.tilesizex);
        int yposition = (int)(((gameObject.GetComponent<Transform>().position.y) / map.tilesizey) + gameObject.GetComponent<Renderer>().bounds.size.y/2);
        int zposition = (int)((gameObject.GetComponent<Transform>().position.z) / map.tilesizez);
        int pathnum = 0;
        Debug.Log("x " + xposition + " y " + yposition + " z " + zposition);
        Vector3 p_tile = new Vector3((int)((gameObject.GetComponent<Transform>().position.x) / map.tilesizex),
            (int)((gameObject.GetComponent<Transform>().position.y) / map.tilesizey),
            (int)((gameObject.GetComponent<Transform>().position.z) / map.tilesizez));

        List<List<Vector3>> generated = new List<List<Vector3>>();
        List<Vector3> addedlist = new List<Vector3>();

        //generated.Add()

        Vector3 neighbour = Vector3.zero;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if ((i == 1) && (j == 1)) {
                    // do nothing, player tile
                }
                else
                {
                    neighbour = new Vector3();
                    neighbour.x = (p_tile.x + i) * map.tilesizex;
                    neighbour.y = (p_tile.y) * map.tilesizey;
                    neighbour.z = (p_tile.z + j) * map.tilesizez;
                    //generated.Add(neighbour);
                    GetAPath(neighbour, bullseye, generated, 1);
                }

            }
        }/*
        float[] maxweights;
        int curpath = 0;
        foreach(List<Vector3> potential in generated)
        {
            float maxweight = potential[potential.Count - 1].y;
            if (potential[curpath].y == maxweight)
            {
                Debug.Log("Path " + curpath + " : " + potential[curpath] + " | maxweight : " + maxweight);
                curpath++;
            }
        }*/
        //for each generated seek target hit and return it
        // for each generated, seek lowest (duplicates osef ?), return this list of vector3








        //        generated.Add(addedlist);
        //GeneratePath(neighbour, 1);

        
        /*for each neighbour, generate path with startweight+1, 
         * generate returns first pos + next one
         * addedtile = GeneratePath()
         */


        // Debug.Log("target : " + t + " decpart : " + decimalpart);
        // Debug.Log("t_tile : " + t_tile);
        return addedlist;
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
                    target.y = gameObject.transform.position.y;
                    target.z += gameObject.GetComponent<Renderer>().bounds.size.z / 2;
                    path = GeneratePath(gameObject.GetComponent<Transform>().position, target, path, 0);
                    moving = true;
                }
                // else click outside map
            }
        }
        if (moving == true)
        {
           // targetPath.y = (int)((target.y) / map.tilesizey);
           // targetPath.z = (int)((target.z) / map.tilesizez);
            float step = speed * Time.deltaTime;
            if ((gameObject.GetComponent<Transform>().position == target))
            {
                moving = false;
            }
            else
            {
                //resulttile = createMatrixPos(map, gameObject, target);
                /* check if on a new tile since last update to skip calculations ?
                 * creatematrix to get the new target
                 * move to it
                 */
               // Debug.Log(" Pos : " + gameObject.GetComponent<Transform>().position + " / Target1 : " + target + " / Target : " + resulttile);
                //targetPath
                gameObject.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(gameObject.transform.position, target, step));
            }
        }
    }

    void ClearMatrix()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                matrix[i][j] = 0;
            }
        }
    }

    Vector3 GetTargetFromMatrix()
    {
        Vector3 resulttile = Vector3.zero;
        //int maxtile = -99;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (matrix[i][j] == 2)
                {
                    resulttile.x = ((x + i-1) * map.tilesizex) + gameObject.GetComponent<Transform>().localScale.x / 2; //player is in the center of the grid
                    resulttile.y = gameObject.GetComponent<Transform>().position.y; // put to player's y later to not move up/down ?
                    resulttile.z = ((z + j-1) * map.tilesizez) + gameObject.GetComponent<Transform>().localScale.z / 2; //player is in the center of the grid
                }
                //Debug.Log("J="+ j + " / " + matrix[0][j] + " : " + matrix[1][j] + " : " + matrix[2][j]);
            }
        }
       // Debug.Log("goto : " + resulttile.x + " / " + resulttile.z);
       // Debug.Log("____________________________________");
        return resulttile;
    }
    
    Vector3 createMatrixPos(Map map, GameObject player, Vector3 target)
    {
        // player's tile position based on ground size and tilesize
        x = (int)((player.GetComponent<Transform>().position.x) / map.tilesizex);
        y = (int)((player.GetComponent<Transform>().position.y) / map.tilesizey);
        z = (int)((player.GetComponent<Transform>().position.z) / map.tilesizez);

        //target's tile position

        targetPath.x = (int)((target.x) / map.tilesizex);
        targetPath.y = (int)((target.y) / map.tilesizey);
        targetPath.z = (int)((target.z) / map.tilesizez);

        // refresh matrix with zeros
        ClearMatrix();

        // Put number on matrix based on axes
        if (targetPath.z < z)
        {
           // Debug.Log("Target down");
            matrix[0][0]++;
            matrix[1][0]++;
            matrix[2][0]++;

            matrix[0][2]--;
            matrix[1][2]--;
            matrix[2][2]--;
        }
        else if (targetPath.z > z)
        {
          //  Debug.Log("Target up");
            matrix[0][0]--;
            matrix[1][0]--;
            matrix[2][0]--;

            matrix[0][2]++;
            matrix[1][2]++;
            matrix[2][2]++;
        }
        else if (targetPath.y == y)
        {
            matrix[2][1]++;
            matrix[0][1]++;
            matrix[1][2]--;
            matrix[1][0]--;
        }

        if (targetPath.x < x)
        {
           // Debug.Log("target left");
            matrix[0][0]++;
            matrix[0][1]++;
            matrix[0][2]++;

            matrix[2][0]--;
            matrix[2][1]--;
            matrix[2][2]--;
        }
        else if (targetPath.x > x)
        {
           // Debug.Log("target right");
            matrix[0][0]--;
            matrix[0][1]--;
            matrix[0][2]--;

            matrix[2][0]++;
            matrix[2][1]++;
            matrix[2][2]++;
        }
        else if (targetPath.x == x)
        {
            matrix[2][1]--;
            matrix[0][1]--;
            matrix[1][2]++;
            matrix[1][0]++;
        }

       // Debug.Log("LU : " + matrix[0][2] +" RU " + matrix[2][2]);
       // Debug.Log("LD : " + matrix[0][0] + " RD " + matrix[2][0]);

        /*Vector3 targetmatrix = GetTargetFromMatrix();
        //Vector3 finalcountdown = Vector3.zero;
        targetPath.x = x + targetmatrix.x;
        targetPath.y = y + targetmatrix.y;
        targetPath.z = z + targetmatrix.z;*/

        return (GetTargetFromMatrix());

        /*Invert if inferior (no)
        - Remove colliders tiles - 
        Go for the biggest number (2)
        if two biggest, calculate next step and judge (rince and repeat )
        go for the center of the chosen tile, minus offset for player's scale.

         */

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Contains("IndestructibleWall"))
        {
            moving = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //Debug.Log("collided yel;o " + col.gameObject.name);
        }
    }
    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.name.Contains("IndestructibleWall"))
        {
            moving = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
           // Debug.Log("cso " + col.gameObject.name);
        }
    }


}
