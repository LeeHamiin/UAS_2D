using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 2f;           // Kecepatan gerak musuh
    public float moveDistance = 3f;    // Jarak bolak-balik

    private Vector3 startPos;
    private int direction = 1;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Gerak horizontal bolak-balik
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        // Jika sudah mencapai batas kiri atau kanan, balik arah
        if (Mathf.Abs(transform.position.x - startPos.x) >= moveDistance)
        {
            direction *= -1;

            // Flip sprite jika perlu (jika punya SpriteRenderer)
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
