using UnityEngine;

public class MovingBars : MonoBehaviour
{
    public float moveDistance = 2f;
    public float speed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * moveDistance;
        transform.position = startPos + new Vector3(x, 0, 0);
    }
}
