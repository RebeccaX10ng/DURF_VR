using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollisionTeleporter : MonoBehaviour
{
    public TeleportationAnchor teleportationAnchor;
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            TeleportRequest request = new TeleportRequest
            {
                destinationPosition = teleportationAnchor.teleportAnchorTransform.position,
                destinationRotation = teleportationAnchor.teleportAnchorTransform.rotation,
                matchOrientation = teleportationAnchor.matchOrientation
            };
            teleportationAnchor.teleportationProvider.QueueTeleportRequest(request);
            Debug.Log("Player teleported to: " + teleportationAnchor.teleportAnchorTransform.position);
        }
    }
}