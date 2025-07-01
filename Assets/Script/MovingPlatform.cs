using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum MoveDirection { Horizontal, Vertical }
    public enum ActivateMode { Always, OnPlayerTouch }

    public MoveDirection moveDirection = MoveDirection.Horizontal;
    public ActivateMode activateMode = ActivateMode.Always;

    public float moveDistance = 3f;
    public float moveSpeed = 2f;

    private Vector3 startPos;
    private bool movingPositive = true;
    private bool isActive = false;

    void Start()
    {
        startPos = transform.position;
        isActive = (activateMode == ActivateMode.Always);
    }

    void Update()
    {
        if (!isActive) return;

        Vector3 direction = moveDirection == MoveDirection.Horizontal ? Vector3.right : Vector3.up;

        if (movingPositive)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, startPos) >= moveDistance)
                movingPositive = false;
        }
        else
        {
            transform.position -= direction * moveSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, startPos) <= 0.1f)
                movingPositive = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            if (activateMode == ActivateMode.OnPlayerTouch)
                isActive = true;

            StartCoroutine(SetPlayerParentDelayed(other.collider.transform));
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.transform.SetParent(null);
        }
    }

    System.Collections.IEnumerator SetPlayerParentDelayed(Transform player)
    {
        yield return new WaitForSeconds(0.05f); // Delay sedikit lebih lama (1/20 detik)
        player.SetParent(this.transform);
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
