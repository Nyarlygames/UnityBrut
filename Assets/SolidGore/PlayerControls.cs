using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControls : MonoBehaviour {
    Transform pt;
    Rigidbody prb;
    NavMeshAgent pnav;
    public List <Vector3> goal = new List <Vector3>(15);
    GameObject radius;
    Transform rpt;
    float speed = 25.0f;

	// Use this for initialization
	void Start () {
        pt = gameObject.GetComponent<Transform>();
        prb = gameObject.GetComponent<Rigidbody>();
        pnav = gameObject.GetComponent<NavMeshAgent>();
        radius = Instantiate(Resources.Load("Radius") as GameObject);
        rpt = radius.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1 << LayerMask.NameToLayer("ground")))
//                if (Physics.Raycast(ray, out hit))
                {
                       Debug.Log(hit.collider.name);
                       Vector3 temp = new Vector3(hit.point.x, hit.point.y + pt.localScale.y, hit.point.z);
                       goal.Add(temp);
                       Debug.Log("added move " + goal[goal.Count-1]);
                }
            }
            // enqueue goal
           
        }
        
        
    }
    

    void FixedUpdate()
    {
         rpt.position = pt.position;
        if ((goal.Count > 0) && (pt.position.x != goal[0].x) && (pt.position.z != goal[0].z))
        {
            Debug.Log("moveto " + goal[0]);
            //  transform.position = Vector3.MoveTowards(pt.position, goal[0], speed * Time.deltaTime);
             prb.MovePosition(Vector3.MoveTowards(pt.position, goal[0], speed * Time.deltaTime));
          //  pnav.destination = goal[0];
            // move towards goal at speed
        }
        else if ((goal.Count > 0) && (pt.position.x == goal[0].x) && (pt.position.z == goal[0].z))//(pt.position.x == goal[0].x) && (pt.position.z == goal[0].z))
        {
            Debug.Log("reached!");
            goal.RemoveAt(0);
            Debug.Log("goal.Count : " + goal.Count);
            // remove queued travel
        }
        else if (goal.Count > 0)
        {
            goal.RemoveAt(0);

        }

    }




}
