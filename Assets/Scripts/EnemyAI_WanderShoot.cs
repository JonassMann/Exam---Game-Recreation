using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_WanderShoot : MonoBehaviour
{
    public float moveSpeed;

    private bool doMove;

    public int changeChance;
    public List<int> dir;
    public LayerMask wall;

    public Vector3 goalPos;

    private void Start()
    {
        dir = new List<int>();
        doMove = true;
        changeChance = 1;
        goalPos = transform.position-transform.up;
    }

    private void Update()
    {
        if (doMove)
            transform.position = Vector3.MoveTowards(transform.position, goalPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, goalPos) < 0.001f)
            ChangeDir();
    }

    private void ChangeDir()
    {
        dir.Clear();
        if (Random.Range(0, 10) < changeChance || Physics2D.Raycast(transform.position, -transform.up, 1.1f, wall))
        {
            if (!Physics2D.Raycast(transform.position, -transform.right, 1.1f, wall)) dir.Add(-90);
            if (!Physics2D.Raycast(transform.position, transform.right, 1.1f, wall)) dir.Add(90);
            if (!Physics2D.Raycast(transform.position, transform.up, 1.1f, wall)) dir.Add(180);

            transform.Rotate(0, 0, dir[Random.Range(0, dir.Count)]);
            changeChance = 1;
        }
        else changeChance++;

        goalPos -= transform.up;
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, -transform.up);
    }
}
