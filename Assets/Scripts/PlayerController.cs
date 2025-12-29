using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 7f;
    private Rigidbody2D rb;

    public Color currentColor;

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

        // Check if the ball falls below the camera
        if (transform.position.y < Camera.main.transform.position.y - 10f)
        {
            Explode();
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
            // Get the color of the obstacle
            Color obstacleColor = collision.GetComponent<SpriteRenderer>().color;

            // Only explode if the colors are different
            if (obstacleColor != currentColor)
            {
                Explode();
            }
            // If colors match, do nothing (pass through)
        }
    }

    void Explode()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
    }
}
