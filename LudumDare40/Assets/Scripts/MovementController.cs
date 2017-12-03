using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{


    public float moveSpeed = 1f;
    private Vector2 moveDirection;

    private bool stuck = false;
    private float stuckTime = 1.5f;
    private float currentStuckTime = 0f;
    // Use this for initialization
    void Awake()
    {
        moveDirection = GetRandomDirection();
    }

    // Update is called once per frame
    void Update()
    {

        if (Global.isGameStarted && !Global.Paused)
        {
            if (stuck)
            {
                currentStuckTime += Time.deltaTime;
                if (currentStuckTime >= stuckTime)
                {
                    moveDirection = GetRandomDirection();
                    currentStuckTime = 0f;
                }
            }
            transform.position += new Vector3(moveDirection.x, moveDirection.y) * moveSpeed * Time.deltaTime;
        }
    }

    Vector3 GetRandomDirection()
    {
        return Random.insideUnitCircle.normalized;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Boundary" || other.collider.gameObject.tag == "Player")
        {
            stuck = true;
            moveDirection = GetRandomDirection();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Boundary" || other.collider.gameObject.tag == "Player")
        {
            stuck = false;
        }
    }


}
