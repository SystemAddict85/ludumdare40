using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHandSpeeds : MonoBehaviour {

    [HideInInspector]
    private float minSpeed = .5f;
    [HideInInspector]
    private float maxSpeed = 4.5f;

    private bool readyToUpdate = false;
    private float randomUpdateTimeMax = 3f;
    private float randomUpdateTimeMin = 2f;
    private float updateTime = 3f;
    private float currentTime = 0f;

    private Animator anim;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        updateTime = GetRandomFloat(randomUpdateTimeMin, randomUpdateTimeMax);        
	}
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        if(currentTime >= updateTime)
        {
            currentTime = 0f;
            anim.speed = GetRandomFloat(minSpeed,maxSpeed);
            updateTime = GetRandomFloat(randomUpdateTimeMin, randomUpdateTimeMax);
        }
	}

    float GetRandomFloat(float min, float max)
    {
        return Random.Range(min, max);
    }
}
