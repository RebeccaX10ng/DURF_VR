using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform player;
    public float offsetX; 
    public float offsetY; 
    public float offsetZ; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            Vector3 newPosition = player.position + new Vector3(offsetX, offsetY, offsetZ);
            player.position = newPosition;
            Debug.Log($"Player has been teleported to position (X:{newPosition.x}, Y:{newPosition.y}, Z:{newPosition.z})");
        }
    }
}
