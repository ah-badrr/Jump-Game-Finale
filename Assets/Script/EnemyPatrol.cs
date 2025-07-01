using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public enum MoveDirection { Horizontal, Vertical }
    public MoveDirection moveDirection = MoveDirection.Horizontal;

    public float moveDistance = 3f;   // Jarak gerak bolak-balik
    public float moveSpeed = 2f;      // Kecepatan gerak

    private Vector3 startPos;
    private bool movingPositive = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Vector3 dir = (moveDirection == MoveDirection.Horizontal) ? Vector3.right : Vector3.up;

        if (movingPositive)
        {
            transform.position += dir * moveSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, startPos) >= moveDistance)
                movingPositive = false;
        }
        else
        {
            transform.position -= dir * moveSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, startPos) <= 0.1f)
                movingPositive = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 dir = (moveDirection == MoveDirection.Horizontal) ? Vector3.right : Vector3.up;
        Vector3 start = Application.isPlaying ? startPos : transform.position;
        Vector3 end = start + dir * moveDistance;

        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(start, 0.1f);
        Gizmos.DrawSphere(end, 0.1f);
    }
}
