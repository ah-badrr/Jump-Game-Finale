using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;           // Objek pemain
    public float smoothSpeed = 0.125f; // Semakin kecil, semakin halus
    public Vector3 offset;             // Offset dari posisi player

    private float minY;                // Batas bawah kamera (posisi awal)

    void Start()
    {
        // Simpan posisi Y awal kamera sebagai batas bawah
        minY = transform.position.y;
        Camera.main.orthographicSize = 5f; // Tinggi dunia 10 unit
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Hitung posisi target Y kamera
            float targetY = player.position.y + offset.y;

            // Cegah kamera turun di bawah posisi awal
            targetY = Mathf.Max(minY, targetY);

            Vector3 targetPos = new Vector3(
                transform.position.x,
                targetY,
                transform.position.z
            );

            transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed);
        }
    }
}
