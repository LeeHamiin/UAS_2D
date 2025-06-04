using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cek apakah objek yang menyentuh adalah player
        if (collision.CompareTag("Player"))
        {
            // Ambil komponen PlayerRespawn dari player dan panggil Respawn
            PlayerRespawn respawnScript = collision.GetComponent<PlayerRespawn>();
            if (respawnScript != null)
            {
                respawnScript.Respawn();
            }
        }
    }
}
