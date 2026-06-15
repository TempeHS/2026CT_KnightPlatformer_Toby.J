using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour
{
    Rigidbody2D playerRb;
    Vector2 startPos;
    Vector3 originalScale;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        startPos = transform.position;
        originalScale = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.linearVelocity = Vector2.zero;
        playerRb.simulated = false;
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(duration);
        transform.position = startPos;
        transform.localScale = originalScale;
        playerRb.simulated = true;
    }
}
