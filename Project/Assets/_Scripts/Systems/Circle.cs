using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public float expandRate = 1.02f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet")) return;
        Destroy(other.gameObject);
        Expand();
    }

    private void Expand()
    {
        transform.DOScale(transform.localScale * expandRate, .1f);
    }
}
