using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerFinish : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            // Dapatkan index scene sekarang
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Cek jika masih ada level berikutnya
            if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                // Pindah ke scene berikutnya
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
            else
            {
                Debug.Log("Sudah di level terakhir!");
                // Kamu bisa tampilkan UI "You Win" di sini
            }
        }
    }
}
