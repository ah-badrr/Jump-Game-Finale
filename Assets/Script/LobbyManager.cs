using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public string levelSceneName = "Level1";

    void Update()
    {
        // Jika pemain menekan spasi, mulai game
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayGame();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(levelSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit();
    }
}
