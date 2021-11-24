using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 goalPos;
    public static bool move;

    void Start()
    {
        goalPos = transform.position;
        move = false;
    }

    void Update()
    {
        if(move)
        {
            if (Vector3.Distance(transform.position, goalPos) < 0.001f)
            {
                transform.position = goalPos;
                move = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, goalPos, Time.deltaTime*moveSpeed);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!move)
        {
            goalPos = transform.position + Vector3.Scale(collision.gameObject.GetComponent<PlayerController>().GetMoveDir(), new Vector3(16, 11, 0));
            move = true;
        }
    }
}
