using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        if (player.position.y > transform.position.y)
        {
            transform.position = new Vector3(0, player.position.y, -10);
        }
    }
}
