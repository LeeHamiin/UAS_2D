using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameContoller gameController;

    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameContoller>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player kena musuh! Game Over.");
            if (gameController != null)
            {
                gameController.GameOverScreen(); // Tampilkan layar Game Over
            }
            else
            {
                Debug.LogWarning("GameContoller tidak ditemukan!");
            }
        }
    }
}
