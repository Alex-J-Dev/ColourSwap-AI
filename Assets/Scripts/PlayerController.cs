using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    public Color currentColor;

    public GameObject explosionPrefab; // assign in inspector

    public static bool isAlive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetRandomColor();

        isAlive = true; // reset flag on scene reload
        rb.isKinematic = false;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    void Update()
    {
        if (!isAlive) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void LateUpdate()
    {
        if (transform.position.y < Camera.main.transform.position.y - 6f)
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
        if (!isAlive) return; // prevent multiple calls
        isAlive = false;

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        GetComponent<SpriteRenderer>().enabled = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        StartCoroutine(RestartGame());
    }

    System.Collections.IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
