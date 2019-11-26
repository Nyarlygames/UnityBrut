using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WispController : MonoBehaviour {

    private NavMeshAgent pnav;
    private Transform pt;
    private Rigidbody prb;
    public Vector3 goal = Vector3.zero;
    public float speed = 10.0f;
    GameObject player_attach;
    Material pmat;
    // Use this for initialization
    void Start ()
    {
        pt = gameObject.GetComponent<Transform>();
        prb = gameObject.GetComponent<Rigidbody>();
        pmat = Resources.Load("img/Players/Materials/player2") as Material;

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                goal = hit.point;
            }
        }
    }

    void FixedUpdate()
    {
        if ((goal != Vector3.zero) && (pt.position != goal))
        {
            transform.position = Vector3.MoveTowards(pt.position, goal, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            player_attach = Instantiate(Resources.Load("Player") as GameObject);
            player_attach.GetComponent<Transform>().Rotate(new Vector3(90.0f,0.0f,0.0f));
            player_attach.name = "Player_Selected";
            player_attach.GetComponent<Renderer>().material = other.GetComponent<HeroSelectorController>().HeroTarget.GetComponent<Renderer>().material;
            Camera.main.GetComponent<GameControl>().camtarget = player_attach.name;
            Camera.main.fieldOfView = 60.0f;
            Vector3 temp = GameObject.Find("Spawn_City").GetComponent<Transform>().position;
            temp.y += player_attach.GetComponent<Transform>().lossyScale.y*2;

            player_attach.GetComponent<NavMeshAgent>().Warp(temp);
            this.enabled = false;

            EnemyControl en = GameObject.Find("Enemy").GetComponent<EnemyControl>();
            en.StartMoving();
        }
    }
    
}
