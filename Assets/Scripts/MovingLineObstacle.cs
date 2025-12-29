using UnityEngine;

public class MovingLineObstacle : MonoBehaviour
{
    public float speed = 2f;
    public float segmentWidth = 2f; // width of each segment
    private Transform[] segments;

    void Start()
    {
        segments = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            segments[i] = transform.GetChild(i);
    }

    void Update()
    {
        float move = speed * Time.deltaTime;
        transform.position += Vector3.right * move; // move right

        // Loop segments individually
        foreach (Transform seg in segments)
        {
            Vector3 pos = seg.position;
            if (pos.x > 4f) // right edge of screen (adjust as needed)
                pos.x -= 8f; // total width (4 segments)
            else if (pos.x < -4f) // left edge
                pos.x += 8f;
            seg.position = pos;
        }
    }
}
