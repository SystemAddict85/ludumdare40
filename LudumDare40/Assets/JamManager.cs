using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamManager : MonoBehaviour {

    public Transform rehearsalStartingSpots;
    private Transform[] startLocations = new Transform[8];
    public List<Guitarist> activeGuits = new List<Guitarist>();
    
	// Use this for initialization
	void Awake () {
        var trans = rehearsalStartingSpots.GetComponentsInChildren<Transform>();
        for (int i = 1; i < trans.Length; ++i)
            startLocations[i - 1] = trans[i];
        
	}

    public void SetupRound()
    {
        PlaceGuitarists();
        StartGame();
    }

    void PlaceGuitarists()
    {
        int i = 0;
        foreach (var guit in activeGuits)
        {
            Destroy(guit.GO);
            guit.GO = Instantiate(Resources.Load("Prefabs/GuitaristPlaying")) as GameObject;
            guit.GO.transform.parent = startLocations[i];
            guit.GO.transform.position = startLocations[i].position; 
            guit.ChangeGameObject();
            ++i;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartGame()
    {
        Global.isGameStarted = true;
    }
}
