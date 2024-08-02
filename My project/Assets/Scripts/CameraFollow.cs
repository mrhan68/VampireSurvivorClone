using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset; // Offset between the camera and the player

    void Start()
    {
        // Initialize the offset based on the initial positions of the camera and the player
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Update the camera's position to follow the player
        if (player != null)
        transform.position = player.position + offset;
    }
}