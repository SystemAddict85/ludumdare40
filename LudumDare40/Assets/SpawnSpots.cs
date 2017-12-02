using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpots : MonoBehaviour {


    public Transform[] TalkingSpots = new Transform[8];
    public Transform NameSpot;
	
    void Awake()
    {
        var trans = GetComponentsInChildren<Transform>();

        for (int i = 1; i < 10; ++i)
        {
            if (i != 9)
                TalkingSpots[i - 1] = trans[i];
            else
                NameSpot = trans[9];
        }
    }	
}
