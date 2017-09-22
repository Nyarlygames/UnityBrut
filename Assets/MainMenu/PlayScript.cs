using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayScript : MonoBehaviour {

    public Button PlayButton;
    public string SceneToLoad="";

    void Start()
    {
        Button btn = PlayButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    

    void TaskOnClick()
    {
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
    }
}
