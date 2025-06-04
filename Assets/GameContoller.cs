using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class GameContoller : MonoBehaviour
{
    public GameObject gameOverScreen;   
    public TMP_Text survivedText;
    private int survivedLevelsCount;

    void Start()
    {
        PlayerHealth.OnPlayerDied += GameOverScreen;
        gameOverScreen.SetActive(false);
    }

    public void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
        survivedText.text = "YOU SURVIVED " + survivedLevelsCount + " LEVEL";
        if (survivedLevelsCount != 1) survivedText.text += "S";
    }

    void Update()
    {
        // Kosong sementara
    }

public void RestartGame()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}

void OnDestroy()
{
    PlayerHealth.OnPlayerDied -= GameOverScreen;
}
public void GameOver()
{
    // Jika kamu tidak ingin menampilkan GameOverScreen dulu, langsung load scene:
    SceneManager.LoadScene("Finish");
}



}
