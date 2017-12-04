using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public string scene;
    public void Awake()
    {
        var man = GameObject.Find("Game Managers");
        if (man != null)
        {
            Destroy(man);
        }
    }
	// Update is called once per frame
	public void StartTheGame () {
        SceneManager.LoadScene(scene);
	}
}
