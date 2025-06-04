using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healAmount = 1;
    public static event Action<int> OnHealthCollect;

    public void Collect()
    {
        OnHealthCollect?.Invoke(healAmount);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        Collect();
    }
}
}
