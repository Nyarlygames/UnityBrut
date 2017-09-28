using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGO : MonoBehaviour {

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
    public float speedx = 0.0f;
    public float speedy = 0.0f;
    public float speedz = 0.0f;
    public float speed = 10.0f;
    public bool moving = false;
    public Vector3 target;


    void Start ()
    {

    }

    public void setUp(int x, int y, int z, int scalex, int scaley, int scalez, string texture, int controltype)
    {
        gameObject.GetComponent<Transform>().localScale = new Vector3(scalex, scaley, scalez);
        gameObject.GetComponent<Transform>().position = new Vector3(x + gameObject.GetComponent<Renderer>().bounds.size.x / 2, y + gameObject.GetComponent<Renderer>().bounds.size.y / 2, z + gameObject.GetComponent<Renderer>().bounds.size.z / 2);
        gameObject.GetComponent<Renderer>().material.mainTexture = Resources.Load(texture) as Texture;
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        control = controltype;
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
                    target = hit.point;
                    target.y = gameObject.transform.position.y;
                    target.z += gameObject.GetComponent<Renderer>().bounds.size.z / 2;
                    moving = true;
                }
                // else click outside map
            }
        }
        if (moving == true)
        {
            float step = speed * Time.deltaTime;
            if ((gameObject.GetComponent<Transform>().position == target))
            {
                moving = false;
                Debug.Log("reached destination");
            }
            else
            {
                /*Vector3 next_pos = Vector3.MoveTowards(playergo.transform.position, target, step);
                


               
                Collider[] hitColliders = Physics.OverlapSphere(next_pos, playergo.GetComponent<Renderer>().bounds.size.x);
                if (hitColliders.Length > 0)
                {
                    for (int i = 0; i < hitColliders.Length; i++)
                    {
                        if (hitColliders[i].name.Contains("IndestructibleWall")) // collision based on name.
                        {
                            moving = false;
                            Debug.Log("stopped by collision");
                        }
                        else
                        {
                            playergo.transform.position = Vector3.MoveTowards(playergo.transform.position, target, step);
                        }
                    }
                }
                else
                {*/
                //gameObject.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(playergo.transform.position, target, step));
                gameObject.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(gameObject.transform.position, target, step));
                Debug.Log("towards : " + Vector3.MoveTowards(gameObject.transform.position, target, step) + " target : " + target);
                // gameObject.GetComponent<Transform>().position = playergo.GetComponent<Transform>().position;
                //}
                // Debug.Log("new pos : " + Vector3.MoveTowards(playergo.transform.position,target, step));
            }
        }
       // else
          //  Debug.Log("lol");
        //gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Contains("IndestructibleWall"))
        {
            moving = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //float step = speed * Time.deltaTime;
            //playergo.GetComponent<Transform>().position = Vector3.MoveTowards(playergo.transform.position, target, -step);
            Debug.Log("collided yel;o " + col.gameObject.name);
        }
    }
    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.name.Contains("IndestructibleWall"))
        {
            moving = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //float step = speed * Time.deltaTime;
            //playergo.GetComponent<Transform>().position = Vector3.MoveTowards(playergo.transform.position, target, -step);
            Debug.Log("cso " + col.gameObject.name);
        }
    }


}
