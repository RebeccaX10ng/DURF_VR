using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform receiver; // 接收传送的目标位置
    private bool playerIsOverlapping = false;
    public Transform playerCamera;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOverlapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOverlapping = false;
        }
    }

    void Update()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = playerCamera.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            // 调试信息
            Debug.Log("Player is overlapping");
            Debug.Log("Portal to Player: " + portalToPlayer);
            Debug.Log("Dot Product: " + dotProduct);

            // 防止反复传送
            if (dotProduct < 0f)
            {
                // 传送玩家到接收位置
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;
                playerCamera.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                playerCamera.position = receiver.position + positionOffset;

                Debug.Log("Player Teleported");
                playerIsOverlapping = false;
            }
        }
    }

}