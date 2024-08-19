using System;
using _Scripts.Managers;
using DG.Tweening;
using UnityEngine;

public class CircleArea : MonoBehaviour
{
    public float expandRate = 1.02f;
    public float maximumExpansion;
    public ResourceType resourceType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet")) return;
        Destroy(other.gameObject);
        Expand();

        GameManager.Instance.resourceManager.GainResource(resourceType, GameManager.Instance.playerController.resourcePerBullet);
        // ResourceManager.OnResourceGained?.Invoke(resourceType);
    }

    // TODO: check if reach the maximum expansion
    private void Expand()
    {
        transform.DOScale(transform.localScale * expandRate, .1f);
    }
}
