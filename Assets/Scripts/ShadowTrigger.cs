using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTrigger : MonoBehaviour
{
    public GameObject Glass;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shadow"))
        {
            Glass.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shadow"))
        {
            Glass.SetActive(false);
        }
    }
}