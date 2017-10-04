using UnityEngine;

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
    public float speedx = 0.0f;
    public float speedy = 0.0f;
    public float speedz = 0.0f;
    public float speed = 2.0f;
    public bool moving = false;
    public Vector3 target;


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
    }

    void Update()
    {
       /* if (control == 1)
        { // mouse & keyboard 
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    target = hit.point;
                    target.y = playergo.transform.position.y;
                    target.z += playergo.GetComponent<Renderer>().bounds.size.z / 2;
                    moving = true;
                }
                // else click outside map
            }
        }
        if (moving == true)
        {
            if (playergo.transform.position == target)
            {
                moving = false;
                Debug.Log("reached destination");
            }
            else
            {
                float step = speed * Time.deltaTime;
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
               // playergo.GetComponent<Transform>().position = Vector3.MoveTowards(playergo.transform.position, target, step);
               // gameObject.GetComponent<Transform>().position = playergo.GetComponent<Transform>().position;
                //}
                // Debug.Log("new pos : " + Vector3.MoveTowards(playergo.transform.position,target, step));
         //   }
      //  }*/
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
