using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;
    private Animator anim;

    private KeyCode lastInput;
    private Stack<KeyCode> inputs;
    private Dictionary<KeyCode, Vector2> directions;
    private bool doMove;

    void Awake()
    {
        MovementSetup();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        doMove = true;
        inputs = new Stack<KeyCode>();
        lastInput = KeyCode.Alpha0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = KeyCode.W;
            inputs.Push(KeyCode.W);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = KeyCode.D;
            inputs.Push(KeyCode.D);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = KeyCode.S;
            inputs.Push(KeyCode.S);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = KeyCode.A;
            inputs.Push(KeyCode.A);
        }


        if (!Input.GetKey(lastInput))
        {
            while (inputs.Count != 0)
            {
                lastInput = inputs.Peek();
                if (Input.GetKey(lastInput))
                    break;
                else inputs.Pop();
            }
            if (inputs.Count == 0) lastInput = KeyCode.Alpha0;
        }
    }

    void FixedUpdate()
    {
        if (!CameraManager.move && doMove)
        {
            rb.velocity = directions[lastInput] * Time.fixedDeltaTime * moveSpeed;
            if (lastInput == KeyCode.Alpha0)
                anim.speed = 0;
            else
            {
                anim.SetInteger("velX", (int)rb.velocity.x);
                anim.SetInteger("velY", (int)rb.velocity.y);
                anim.speed = 1;
            }
        }
    }

    public Vector3 GetMoveDir()
    {
        return directions[lastInput];
    }

    private void MovementSetup()
    {
        directions = new Dictionary<KeyCode, Vector2>();
        directions[KeyCode.W] = new Vector2(0, 1);
        directions[KeyCode.A] = new Vector2(-1, 0);
        directions[KeyCode.S] = new Vector2(0, -1);
        directions[KeyCode.D] = new Vector2(1, 0);
        directions[KeyCode.Alpha0] = new Vector2(0, 0);
    }
}
