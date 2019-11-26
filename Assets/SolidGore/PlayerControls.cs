using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControls : MonoBehaviour {
    Transform pt;
    Rigidbody prb;
    NavMeshAgent pnav;
    public List <Vector3> goal = new List <Vector3>(15);
    public List<GameObject> goal_nav = new List<GameObject>();
    GameObject radius;
    Transform rpt;
    float speed = 25.0f;
    public Sprite navSprite;

    // Use this for initialization
    void Start () {
        pt = gameObject.GetComponent<Transform>();
        prb = gameObject.GetComponent<Rigidbody>();
        pnav = gameObject.GetComponent<NavMeshAgent>();
        radius = Instantiate(Resources.Load("Radius") as GameObject);
        rpt = radius.GetComponent<Transform>();
        navSprite = Resources.Load<Sprite>("img/Arrow");
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
                {
                    //goal.Clear();
                    Vector3 temp = new Vector3(hit.point.x, hit.point.y + pt.localScale.y, hit.point.z);
                    goal.Add(temp);
                    GameObject New_Dest = new GameObject("Nav " + goal.Count);
                    New_Dest.AddComponent<SpriteRenderer>();
                    New_Dest.GetComponent<SpriteRenderer>().sprite = navSprite;
                    New_Dest.transform.position = temp;
                    goal_nav.Add(New_Dest);
                    Debug.Log("added move " + goal[goal.Count - 1]);
                    pnav.destination = goal[0];
                }
            }
            else
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1 << LayerMask.NameToLayer("ground")))
                {
                    goal.Clear();
                    goal_nav.ForEach(e => Destroy(e));
                    goal_nav.Clear();
                    Vector3 temp = new Vector3(hit.point.x, hit.point.y + pt.localScale.y, hit.point.z);
                    goal.Add(temp);
                    GameObject New_Dest = new GameObject("Nav " + goal.Count);
                    New_Dest.AddComponent<SpriteRenderer>();
                    New_Dest.GetComponent<SpriteRenderer>().sprite = navSprite;
                    New_Dest.transform.position = temp;
                    goal_nav.Add(New_Dest);

                    Debug.Log("forced move " + goal[goal.Count - 1]);
                    pnav.destination = goal[0];
                }

            }
        }
        rpt.position = pt.position;

        // check destination reached
        if (!pnav.pathPending)
        {
            if (pnav.remainingDistance <= pnav.stoppingDistance)
            {
                if (!pnav.hasPath || pnav.velocity.sqrMagnitude == 0f)
                {
                    if (goal.Count > 0)
                    {
                        goal.RemoveAt(0);
                        Destroy(goal_nav[0]);
                        goal_nav.RemoveAt(0);
                        if (goal.Count > 0)
                            pnav.destination = goal[0];
                        Debug.Log("goal.Count : " + goal.Count);
                    }
                }
            }
        }

        /*if ((goal.Count > 0) && (pt.position.x != goal[0].x) && (pt.position.z != goal[0].z))
        {
            // move towards goal at speed
        }
        else*/
        /*if ((goal.Count > 0) && (pt.position.x == goal[0].x) && (pt.position.z == goal[0].z))
        {
            goal.RemoveAt(0);
            if (goal.Count > 0)
                pnav.destination = goal[0];
            Debug.Log("goal.Count : " + goal.Count);
        }*/


    }
    
}
