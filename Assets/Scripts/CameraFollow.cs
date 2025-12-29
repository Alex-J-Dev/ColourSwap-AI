using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        if (PlayerController.isAlive && player.position.y > transform.position.y)
        {
            transform.position = new Vector3(0, player.position.y, -10);
        }
    }
}
