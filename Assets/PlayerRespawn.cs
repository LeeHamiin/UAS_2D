using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerRespawn : MonoBehaviour
{
    private Vector3 spawnPoint;

    void Start()
    {
        spawnPoint = transform.position;
    }

public void Respawn()
{
    // Reload scene saat respawn
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

}
