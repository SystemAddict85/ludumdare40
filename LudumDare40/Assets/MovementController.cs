using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {


    public float moveSpeed = 1f;
    private Vector2 moveDirection;
	// Use this for initialization
	void Awake () {
        moveDirection = GetRandomDirection();
	}
	
	// Update is called once per frame
	void Update () {
        if (Global.isGameStarted)
        {
            transform.position += new Vector3(moveDirection.x, moveDirection.y) * moveSpeed * Time.deltaTime;
        }
	}

    Vector3 GetRandomDirection()
    {
        return Random.insideUnitCircle.normalized;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.gameObject.tag == "Boundary")
        {
            moveDirection = GetRandomDirection();
        }
    }

    void OnCollissionStay2D(Collision2D other)
    {
        moveDirection = GetRandomDirection();
    }
        
}
