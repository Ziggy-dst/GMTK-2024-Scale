using System;
using _Scripts.Managers;
using DG.Tweening;
using UnityEngine;

public class CircleEdge : MonoBehaviour
{
    private ResourceType _resourceType;
    private CircleArea _circleArea;

    private void Start()
    {
        _circleArea = transform.parent.GetComponent<CircleArea>();
        _resourceType = _circleArea.resourceType;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet")) return;
        Destroy(other.gameObject);
        _circleArea.Expand();

        GameManager.Instance.resourceManager.GainResource(_resourceType, GameManager.Instance.playerController.resourcePerBullet);
        // ResourceManager.OnResourceGained?.Invoke(resourceType);
    }
}
