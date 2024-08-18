using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public Collider2D circle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) circle.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) circle.enabled = true;
    }
}
