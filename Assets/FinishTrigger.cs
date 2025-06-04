using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishTrigger : MonoBehaviour
{
    public GameContoller gameController;

private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("Finish Triggered by: " + other.name);
        SceneManager.LoadScene("Finish"); // Ganti dengan nama scene yang kamu tuju
    }
}
}

