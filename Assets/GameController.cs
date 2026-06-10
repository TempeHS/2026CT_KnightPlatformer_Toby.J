using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour
{
    Rigidbody2D playerRb;
    Vector2 startPos;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        startPos = transform.position;
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
        playerRb.linearVelocity = new Vector2(0, 0);
        playerRb.simulated = false;
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = startPos;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
    }
}
