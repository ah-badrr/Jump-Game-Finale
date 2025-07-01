using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 7f;
    public float moveSpeed = 5f;
    public float downSpeed = 5f;

    public Transform groundCheck;      // Titik untuk pengecekan tanah
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;      // Layer tanah

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool facingRight = true; // Untuk melacak arah saat ini

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Cek apakah menyentuh tanah
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Gerakan horizontal
        float moveX = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -moveSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = moveSpeed;
        }

        rb.velocity = new Vector2(moveX, rb.velocity.y);

        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }

        // Lompat jika sedang di tanah
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Turun saat tekan S
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(rb.velocity.x, -downSpeed);
        }

        ClampPositionWithinCamera();
    }

    void ClampPositionWithinCamera()
    {
        Camera cam = Camera.main;
        float halfWidth = cam.orthographicSize * cam.aspect;

        float minX = cam.transform.position.x - halfWidth;
        float maxX = cam.transform.position.x + halfWidth;

        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, minX, maxX);
        transform.position = clampedPos;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Balik arah sumbu X
        transform.localScale = scale;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            GameManager.Instance.ReachCheckpoint(other.transform.position);
        } else if (other.CompareTag("End"))
        {
            GameManager.Instance.TriggerVictory();
        }
    }
}
