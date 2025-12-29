using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    public Color currentColor;

    public GameObject explosionPrefab; // assign in inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetRandomColor();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Die if player falls off screen
        if (transform.position.y < Camera.main.transform.position.y - 10f)
        {
            Die();
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    void SetRandomColor()
    {
        int rand = Random.Range(0, 4);

        switch (rand)
        {
            case 0: currentColor = Color.cyan; break;
            case 1: currentColor = Color.yellow; break;
            case 2: currentColor = Color.magenta; break;
            case 3: currentColor = Color.red; break;
        }

        GetComponent<SpriteRenderer>().color = currentColor;
    }

    bool ColorsMatch(Color a, Color b)
    {
        float tolerance = 0.01f;
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ColorChanger"))
        {
            SetRandomColor();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Coin"))
        {
            ScoreManager.score++;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Obstacle"))
        {
            Color obstacleColor = collision.GetComponent<SpriteRenderer>().color;

            if (!ColorsMatch(obstacleColor, currentColor))
            {
                Die();
            }
        }
    }

    void Die()
    {
        // Spawn explosion
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Hide the player
        GetComponent<SpriteRenderer>().enabled = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        // Restart after 1 second
        StartCoroutine(RestartGame());
    }

    System.Collections.IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
