using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour {

    public Canvas ProfileViewCanvas;
    public Canvas GraphViewCanvas;
    public Button ProfileTab;
    public Button GraphTab;

    // Use this for initialization
    void Start ()
    {
        Button temp = ProfileTab.GetComponent<Button>();
        temp.onClick.AddListener(ProfileClick);
        temp = GraphTab.GetComponent<Button>();
        temp.onClick.AddListener(GraphClick);

        ProfileViewCanvas.enabled = false;
        GraphViewCanvas.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {

    }

    void ProfileClick()
    {
        ProfileViewCanvas.enabled = true;
        GraphViewCanvas.enabled = false;
    }
    void GraphClick()
    {
        ProfileViewCanvas.enabled = false;
        GraphViewCanvas.enabled = true;
    }
}
