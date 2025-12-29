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

        if (collision.CompareTag("Obstacle"))
        {
            if (collision.GetComponent<SpriteRenderer>().color != currentColor)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
    }
}
