using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f;
    public float destroyDelay = 2f;
    public float shakeDuration = 0.4f;
    public float shakeAmount = 0.1f;
    public float respawnDelay = 2f;

    private Rigidbody2D rb;
    private Vector3 originalPos;
    private Quaternion originalRot;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (isFalling) return;

        if (other.collider.CompareTag("Player"))
        {
            // Pastikan player menyentuh dari atas
            ContactPoint2D contact = other.contacts[0];
            bool fromAbove = contact.normal.y < -0.5f;

            if (fromAbove)
            {
                isFalling = true;
                StartCoroutine(ShakeThenFall());
            }
        }
    }

    System.Collections.IEnumerator ShakeThenFall()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            Vector3 offset = new Vector3(
                Random.Range(-shakeAmount, shakeAmount),
                Random.Range(-shakeAmount, shakeAmount),
                0f
            );
            transform.position = originalPos + offset;
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos; // reset posisi
        yield return new WaitForSeconds(fallDelay - shakeDuration);
        StartFalling();
    }

    void StartFalling()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Invoke(nameof(BeginRespawn), destroyDelay);
    }

    void BeginRespawn()
    {
        // Matikan platform dulu (invisible, tidak bisa disentuh)
        gameObject.SetActive(false);
        Invoke(nameof(Respawn), respawnDelay);
    }

    void Respawn()
    {
        // Reset posisi, rotasi, velocity
        transform.position = originalPos;
        transform.rotation = originalRot;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Aktifkan kembali platform
        gameObject.SetActive(true);
        isFalling = false;
    }
}
